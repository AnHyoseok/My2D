using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
namespace My2D
{

    public class Damageable : MonoBehaviour
    {
        #region Variables

        private Animator animator;

        //데미지 입을때 등록된 함수 호출 
        public UnityAction<float, Vector2> hitAction;

        //최대체력
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get
            { return maxHealth; }
            set
            { maxHealth = value; }
        }

        //현재 체력
        private float currentHealth;
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;
                //죽음 처리
                if (currentHealth <= 0)
                {
                    //다이
                    IsDeath = true;
                    Debug.Log("죽음");
                }
            }
        }

        //죽음체크
        private bool isDeath;
        public bool IsDeath
        {
            get
            { return isDeath; }
            set
            {
                isDeath = value;
                //애니메이션
                animator.SetBool(AnimationString.IsDeath, value);

            }
        }

        //무적모드
        private bool isInvincible = false;
        [SerializeField] private float invincibleTimer = 2f;
        private float countdown = 0f;

        //
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);

            }
            set
            {

                animator.SetBool(AnimationString.LockVelocity, value);
            }
        }

        #endregion

        void Awake()
        {
            //참조
            animator = GetComponent<Animator>();


        }

        private void Start()
        {
            //초기화
            currentHealth = maxHealth;
            countdown = invincibleTimer;
            isDeath = false;
        }

        private void Update()
        {
            //무적상태이면 무적 타이머를 돌린다
            if (isInvincible)
            {
                if (countdown <= 0f)
                {
                    isInvincible = false;
                    //타이머 초기화
                    countdown = invincibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        //TakeDamage
        public void TakeDamage(float damage, Vector2 knocback)
        {
            if (!IsDeath && !isInvincible)
            {
                //무적모드 초기화
                isInvincible = true;

                //데미지 전의 hp
                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}의 현재 체력은{currentHealth}");

                LockVelocity = true;
                //애니메이션
                animator.SetTrigger(AnimationString.HitTrigger);

                ////데미지효과
                //if (hitAction != null)
                //{
                //    hitAction.Invoke(damage, knocback);
                //}

                float realDamage = beforeHealth - currentHealth;

                hitAction?.Invoke(realDamage, knocback);
                CharacterEvent.characterDamaged?.Invoke(gameObject, realDamage);
            }
        }

        public bool Heal(float restrore)
        {
            if (CurrentHealth >= MaxHealth)
            {
                return false;
            }

            //힐 전의 hp
            float beforeHealth = CurrentHealth;

            currentHealth += restrore;
            currentHealth = Mathf.Clamp(currentHealth, 0f, MaxHealth);

            //실제 힐 hp값
            float realHealth = currentHealth - beforeHealth;

            CharacterEvent.characterHealth?.Invoke(gameObject, realHealth);

            Debug.Log($"{transform.name}의 현재 체력은{currentHealth}");

            return true;
        }

    }
}