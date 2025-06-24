using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverEffect : MonoBehaviour
{
    public CameraShake cameraShake; // 카메라 쉐이크 스크립트를 연결합니다.
    public Image blackScreen;
    public float fadeDuration = 1.0f;
    public float shakeDuration = 0.5f;

    void Start()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // 맵 흔들림 시작
        yield return StartCoroutine(cameraShake.Shake());

        // 페이드 아웃 시작
        float elapsed = 0.0f;
        Color color = blackScreen.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        // 페이드 아웃 완료 후 추가로 필요한 동작을 여기에 작성
    }
}
