using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

namespace My2D
{

    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;
      
        private Canvas canvas;

        [SerializeField] private Vector3 healthTextOffset = Vector3.zero;
        #endregion

        private void Awake()
        {
            //참조
            canvas = FindObjectOfType<Canvas>();

        }

        private void OnEnable()
        {
            //캐릭터 관련 이벤트 함수 등록
            CharacterEvent.characterDamaged += CharactterTakeDamage;
            CharacterEvent.characterHealth += CharactterHealed;
        }

        void OnDisable()
        {
            //캐릭터 관련 이벤트 함수 제거
            CharacterEvent.characterDamaged -= CharactterTakeDamage;
            CharacterEvent.characterHealth -= CharactterHealed;

        }

        public void CharactterTakeDamage(GameObject character, float damage)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
            //damageTextPrefab 스폰
            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();
        }
        public void CharactterHealed(GameObject character, float restrore)
        {
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
            //damageTextPrefab 스폰
            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restrore.ToString();
        }
    }

}