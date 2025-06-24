using UnityEngine;

public class Rock : MonoBehaviour
{
    private DestroyManager destroyManager;

    private AudioSource audioSource;
    public AudioClip breaking;

    private void Start()
    {
        destroyManager = FindObjectOfType<DestroyManager>();
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트가 없으면 추가
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            PlaySound(breaking);
            gameObject.SetActive(false);
            destroyManager.CheckMissionStatus(); // 소행성 비활성화될 때마다 상태를 확인합니다.
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
