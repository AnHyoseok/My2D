using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;


        //플레이어 걷기 속도
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float airSpeed = 2f;


        //점프높이
        [SerializeField] private float jumpForce = 5f;



        AnimationString AnimationString;

        //플레이어 속도 변화
        public float CurrentMoveSpeed
        {
            get
            {
                if (CanMove)
                {

                    if (IsMove)
                    {
                        if (!touchingDirections.IsGround)
                        {
                            return airSpeed;
                        }
                        if (IsRun)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0; //움직이지 못할때
                }

            }
        }
        //이동여부
        public bool CanMove
        {
            get
            {
                return animator.GetBool(AnimationString.CanMove);
            }

        }

        //플레이어 이동과 관련된 입력값
        private Vector2 inputMove;

        //걷기
        [SerializeField] private bool isMove = false;
        public bool IsMove
        {
            get { return isMove; }
            set
            {
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            }
        }

        //달리기
        [SerializeField] private bool isRun = false;
        public bool IsRun
        {
            get { return isRun; }
            set
            {
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }
        //좌우반전
        [SerializeField] private bool isFacingRight = true;

        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                //반전
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value;
            }
        }
        #endregion

        private void Awake()
        {   //참조
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();
        }

        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2D.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.velocity.y);
            animator.SetFloat("YVelocity", rb2D.velocity.y);
        }



        void SetFacingDirection(Vector2 moveInput)
        {

            //오른쪽바라보기
            if (moveInput.x > 0f && IsFacingRight == false)
            {
                IsFacingRight = true;
            }
            //왼쪽바라보기
            else if (moveInput.x < 0f && IsFacingRight == true)
            {
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            IsMove = (inputMove != Vector2.zero);

            //방향전환
            SetFacingDirection(inputMove);

        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsRun = true;
            }
            else if (context.canceled)
            {
                IsRun = false;
            }
        }

        // 점프 처리
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && touchingDirections.IsGround)
            {
                rb2D.velocity = new Vector2(0f, jumpForce);

                animator.SetTrigger(AnimationString.JumpTrigger);

            }
        }

        // 공격 처리
        public void OnAttack(InputAction.CallbackContext context)
        {
            //지상공격
            if (context.started && touchingDirections.IsGround && !isMove)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);

            }
        }


    }
}