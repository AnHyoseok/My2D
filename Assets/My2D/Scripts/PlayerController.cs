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



        //플레이어 걷기 속도
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float runSpeed = 8f;


        //플레이어 속도 변화
        public float CurrentMoveSpeed
        {
            get
            {
                if (IsMove)
                {
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

        }

        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2D.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.velocity.y);

        }

        void SetFacingDirection(Vector2 moveInput)
        {
            Debug.Log(isFacingRight);
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

    }
}