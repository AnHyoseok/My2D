using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{

    //캐릭터와 관련된 이벤트 함수 관리하는 클래스 
    public class CharacterEvent : MonoBehaviour
    {
        //캐릭터가 데미지를 입을때 등록된 함수 호출
        public static UnityAction<GameObject, float> characterDamaged;

        //캐릭터가 체력을 회복할때 등록된 함수 호출
        public static UnityAction<GameObject, float> characterHealth;

        //....
    }

}