using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{

    public class TouchingDirections : MonoBehaviour
    {
        #region variables
        private CapsuleCollider2D touchingCollider;
        [SerializeField] private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        //[SerializeField] private float ceilingDistance = 0.05f;
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        //private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
        [SerializeField] private bool isGround;
        private Rigidbody2D rb2D;
        private Animator animator;
  


        public bool IsGround
        {
            get { return isGround; }
            private set
            {
                isGround = value;
                animator.SetBool(AnimationString.IsGround, isGround);
            }
        }
    /*    public bool IsCeiling
        {
            get { return IsCeiling; }
            private set
            {
                IsCeiling = value;
                animator.SetBool(AnimationString.IsCeiling, IsCeiling);
            }
        }
*/
        #endregion
        private void Awake()
        {
            //ÂüÁ¶
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            touchingCollider = GetComponent<CapsuleCollider2D>();
        }


        private void FixedUpdate()
        {
            IsGround = (touchingCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance)) > 0;
            //IsCeiling = (touchingCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance)) > 0;
    
        }

    }

}