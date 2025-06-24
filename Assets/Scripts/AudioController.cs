using UnityEngine;

public class AudioController : MonoBehaviour
{
    // AudioSource 컴포넌트를 public 변수로 선언
    public AudioSource audioSource;

    // 게임 시작 시 오디오를 재생하는 함수
    void Start()
    {
        // AudioSource가 null이 아닌지 확인
        if (audioSource != null)
        {
            // 오디오 재생
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource가 할당되지 않았습니다.");
        }
    }
}
