using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TestStateBehaviour : StateMachineBehaviour
{
   override public  void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태에 진입할 때 호출됩니다.
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태가 업데이트될 때 호출됩니다.
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태를 빠져나갈 때 호출됩니다.
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 상태가 이동할 때 호출됩니다.
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 역방향 키네마틱(Inverse Kinematics) 작업을 수행할 때 호출됩니다.
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        // 상태 머신에 진입할 때 호출됩니다.
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        // 상태 머신을 빠져나갈 때 호출됩니다.
    }
}
