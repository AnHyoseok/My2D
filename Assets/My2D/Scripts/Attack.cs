using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    public class Attack : MonoBehaviour
    {
        //공격력
        [SerializeField] private float attacKDamege = 10f;
        //충돌 체크해서 공격력 만큼 데미지 준다 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //데미지 입는 객체 찾기 
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(attacKDamege);
                //Debug.Log($"{collision.name} {damageable.CurrentHealth}데미지를 입었다");
               
            }

        }


    }

}


