using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace My2D
{

    public class HealthText : MonoBehaviour
    {
        #region Vraiable
        private TextMeshProUGUI textHealth;
        public RectTransform textTransfrom;


        //이동
        [SerializeField] private float moveSpeed = 5f;

        //페이드 효과
        private Color startColor;
        public float fadeTimer = 1f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            //참조
            textHealth = GetComponent<TextMeshProUGUI>();
            textTransfrom = GetComponent<RectTransform>();

            //초기화
            startColor = textHealth.color;
            countdown = fadeTimer;
        }

        private void Update()
        {
            //이동 
            textTransfrom.position += Vector3.up * moveSpeed * Time.deltaTime;

            countdown -= Time.deltaTime*0.5f;

            float newAlpha = startColor.a * (countdown / fadeTimer);
            textHealth.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            //페이드 타임 끝
            if (countdown <= 0f)
            {
                Destroy(gameObject);

            }
        }

    }
}