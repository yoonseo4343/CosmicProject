using UnityEngine;

public class Rock : MonoBehaviour
{
    private DestroyManager destroyManager;

    private AudioSource audioSource;
    public AudioClip breaking;

    private void Start()
    {
        destroyManager = FindObjectOfType<DestroyManager>();
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            PlaySound(breaking);
            gameObject.SetActive(false);
            destroyManager.CheckMissionStatus(); // ���༺ ��Ȱ��ȭ�� ������ ���¸� Ȯ���մϴ�.
        }
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
