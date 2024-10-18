using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    public class PickUpHealth : MonoBehaviour
    {

        #region Variable
        //힐 - 회복량ㄷ
        [SerializeField] private float restorHealth = 20f;

        [SerializeField] private Vector3 rotateSpeed = new Vector3(0f, 180f, 0f);
        #endregion

        void Update()
        {
            //회전
            transform.eulerAngles += rotateSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                bool isHeal = damageable.Heal(restorHealth);

                if (isHeal)
                {
                    damageable.Heal(restorHealth);
                    Destroy(gameObject);
                }
            
            }
          
        
        }
    }

}