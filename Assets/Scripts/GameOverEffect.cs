using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverEffect : MonoBehaviour
{
    public CameraShake cameraShake; // ī�޶� ����ũ ��ũ��Ʈ�� �����մϴ�.
    public Image blackScreen;
    public float fadeDuration = 1.0f;
    public float shakeDuration = 0.5f;

    void Start()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // �� ��鸲 ����
        yield return StartCoroutine(cameraShake.Shake());

        // ���̵� �ƿ� ����
        float elapsed = 0.0f;
        Color color = blackScreen.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        // ���̵� �ƿ� �Ϸ� �� �߰��� �ʿ��� ������ ���⿡ �ۼ�
    }
}
