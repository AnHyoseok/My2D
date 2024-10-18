using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    public class Attack : MonoBehaviour
    {
        //공격력
        [SerializeField] private float attacKDamege = 10f;

        public Vector2 knockback = Vector2.zero;
   


        //충돌 체크해서 공격력 만큼 데미지 준다 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //데미지 입는 객체 찾기 
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                //knockback의 방향 설정
                Vector2 deliveredKnockback = (transform.parent.localScale.x >0) ? knockback : new Vector2(-knockback.x, knockback.y);

                damageable.TakeDamage(attacKDamege,knockback);

         ;

                //Debug.Log($"{collision.name} {damageable.CurrentHealth}데미지를 입었다");

            }

        }


    }

}


