using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorImageSwitcher : MonoBehaviour
{
    public Texture2D image1;  // 첫 번째 이미지
    public Texture2D image2;  // 두 번째 이미지

    private Renderer monitorRenderer;  // 모니터 모델의 렌더러
    private bool isSwitched = false;  // 이미지 전환 여부

    void Start()
    {
        // 모니터 모델의 렌더러를 가져옴
        monitorRenderer = GetComponent<Renderer>();
        // 첫 번째 이미지로 초기화
        monitorRenderer.material.mainTexture = image1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // 디버그 로그 추가

        // 플레이어와 충돌 시 이미지를 전환
        if (other.CompareTag("PlayerHand"))
        {
            Debug.Log("Player detected, switching image."); // 디버그 로그 추가
            SwitchImage();
        }
    }

    public void SwitchImage()
    {
        // 이미지를 한 번만 전환
        if (!isSwitched)
        {
            monitorRenderer.material.mainTexture = image2;
            isSwitched = true;
            Debug.Log("Image switched."); // 디버그 로그 추가
        }
    }
}
