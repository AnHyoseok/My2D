using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//animator bool형 파라미터를 제어하는 클래스
namespace My2D
{
    public class SetBoolBehavior : StateMachineBehaviour
    {

        #region Variables
        public string boolName;
        public bool valueEnter;
        public bool valueExit;

        public bool updateOnstate;
        public bool updateOnstateMachine;
        #endregion

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnstate)
            {
                animator.SetBool(boolName, valueEnter);
            }
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnstate)
            {
                animator.SetBool(boolName, valueExit);
            }
        }

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnstateMachine)
            {
                animator.SetBool(boolName, valueEnter);
            }
        }
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnstateMachine)
            {
                animator.SetBool(boolName, valueExit);
            }
        }
    }

}