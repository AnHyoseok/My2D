using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{

    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        public GameObject arrowPrefab;
        public Transform firePoint;
        #endregion



        //발사체(화살) 발사
        public void FireProjectile()
        {
            Debug.Log("화살 발사");
            GameObject projectile = Instantiate(arrowPrefab, firePoint.position, arrowPrefab.transform.rotation);
            Destroy(projectile, 3f);
            //화살의 방향 결정
            Vector3 originScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                originScale.x * transform.localScale.x > 0 ? 1 : -1,
                originScale.y,
                originScale.z);
        
        }
     
    }
}