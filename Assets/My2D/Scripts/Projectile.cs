using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    public class Projectile : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //이동
        [SerializeField] private Vector2 moveSpeed = new Vector2(5f, 0f);

        //데미지 
        [SerializeField] private float attackDamage = 10f;
        [SerializeField] private Vector2 knockback = new Vector2(0f, 0f);

        //임팩트프리팹
        public GameObject bowImpactPrepab;
        #endregion

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb2D.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //데미지 입는 객체 찾기 
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                //knockback의 방향 설정
                Vector2 deliveredKnockback = (transform.localScale.x > 0) ? knockback : new Vector2(-knockback.x, knockback.y);
                damageable.TakeDamage(attackDamage, deliveredKnockback);

                //데미지 이펙트
                GameObject effectGo = Instantiate(bowImpactPrepab, transform.position, Quaternion.identity);
                Destroy(effectGo, 0.5f);
                Debug.Log($"{collision.name} {damageable.CurrentHealth}데미지를 입었다");

                //화살 킬
                Destroy(gameObject);
            }

        }



    }

}