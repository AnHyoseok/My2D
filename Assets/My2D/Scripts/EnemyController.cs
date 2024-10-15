using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace My2D
{

    public class EnemyController : MonoBehaviour
    {
        #region Variables
        Animator animator;
        Rigidbody2D rb2D;
        private TouchingDirections touchingDirections;
        //플레이어 감지
        public DetectionZone detectionZone;

        //이동
        [SerializeField] private float runSpeed = 4f;
        //이동방향
        private Vector2 directionVector = Vector2.right;

        //이동가능 방향
        public enum WalkableDirection { Left, Right }
        //현재 이동 방향
        private WalkableDirection walkDirection = WalkableDirection.Right;
        public WalkableDirection WalkDirection
        {

            get { return walkDirection; }
            private set
            {
                //이미지 플립
                transform.localScale *= new Vector2(-1, 1);

                //실제이동하는 방향값 
                if(value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }
                else if ( value == WalkableDirection.Right)
                {
                    directionVector = Vector2.right;
                }
                

                walkDirection = value;
            }
        }

        //공격 타겟 설정
        [SerializeField] private bool hasTarget = false;
        public bool HasTarget
        {
        
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        //이동 가능상태 / 불가능 상태 - 이동 제한
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }

            
        }
        //감속 계수 
        [SerializeField] private float stopRate = 0.2f;
        #endregion


        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            touchingDirections = GetComponent<TouchingDirections>();

            
        }

        private void Update()
        {
            //적 감지 충돌체의 리스트 개수가 0보다 크면 적이 감지 된 것이다
           HasTarget = (detectionZone.detectedColliders.Count > 0);
        }

        private void FixedUpdate()
        {
            if (touchingDirections.IsWall && touchingDirections.IsGround)
            {
                //반전
                Flip();
            }

         

            //이동
            if (CanMove)
            {
                rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);
            }
            else
            {
                rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x,0f,stopRate), rb2D.velocity.y);
            }
        }

        void Flip()
        {
            if (walkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;  // 속성을 통해 설정
            }
            else if (walkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;   // 속성을 통해 설정
            }
            else
            {
                Debug.Log("에러");
            }
        }
    }

}