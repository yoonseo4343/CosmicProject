using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorImageSwitcher : MonoBehaviour
{
    public Texture2D image1;  // ù ��° �̹���
    public Texture2D image2;  // �� ��° �̹���

    private Renderer monitorRenderer;  // ����� ���� ������
    private bool isSwitched = false;  // �̹��� ��ȯ ����

    void Start()
    {
        // ����� ���� �������� ������
        monitorRenderer = GetComponent<Renderer>();
        // ù ��° �̹����� �ʱ�ȭ
        monitorRenderer.material.mainTexture = image1;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // ����� �α� �߰�

        // �÷��̾�� �浹 �� �̹����� ��ȯ
        if (other.CompareTag("PlayerHand"))
        {
            Debug.Log("Player detected, switching image."); // ����� �α� �߰�
            SwitchImage();
        }
    }

    public void SwitchImage()
    {
        // �̹����� �� ���� ��ȯ
        if (!isSwitched)
        {
            monitorRenderer.material.mainTexture = image2;
            isSwitched = true;
            Debug.Log("Image switched."); // ����� �α� �߰�
        }
    }
}
