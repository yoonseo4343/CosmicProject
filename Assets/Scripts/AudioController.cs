using UnityEngine;

public class AudioController : MonoBehaviour
{
    // AudioSource ������Ʈ�� public ������ ����
    public AudioSource audioSource;

    // ���� ���� �� ������� ����ϴ� �Լ�
    void Start()
    {
        // AudioSource�� null�� �ƴ��� Ȯ��
        if (audioSource != null)
        {
            // ����� ���
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
