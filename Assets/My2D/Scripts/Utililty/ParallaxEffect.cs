using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace My2D
{

    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public new Camera  camera;            // 카메라
        public Transform followTarget;   // 플레이어

        // 여러 배경을 처리하기 위한 리스트
        public List<Transform> backgrounds;

        // 각 배경의 시작 위치와 Z 좌표 저장
        private Vector2[] startingPositions;
        private float[] startingZ;

        // 카메라와 시작지점의 거리
        Vector2 CamMoveSinceStart => (Vector2)camera.transform.position;
        #endregion

        void Start()
        {
            // 각 배경의 시작 위치와 Z 좌표 초기화
            startingPositions = new Vector2[backgrounds.Count];
            startingZ = new float[backgrounds.Count];

            for (int i = 0; i < backgrounds.Count; i++)
            {
                startingPositions[i] = backgrounds[i].position;
                startingZ[i] = backgrounds[i].localPosition.z;
            }
        }

        void Update()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                float zDistanceFromTarget = backgrounds[i].position.z - followTarget.position.z;

                // 클리핑 평면 계산
                float clippingPlane = camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane);

                // Parallax Factor 계산
                float parallaxFactor = Mathf.Abs(zDistanceFromTarget) / clippingPlane;

                // 새로운 위치 계산
                Vector2 newPosition = startingPositions[i] + CamMoveSinceStart * parallaxFactor;

                // 배경의 위치 업데이트
                backgrounds[i].position = new Vector3(newPosition.x, newPosition.y, startingZ[i]);
            }
        }
    }

}
