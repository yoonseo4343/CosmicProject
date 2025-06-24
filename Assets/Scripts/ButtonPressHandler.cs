using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ButtonPressHandler : MonoBehaviour
{
    public GameObject mapPicture; // 활성화/비활성화할 MapPicture 오브젝트
    public GameObject timerObject; // 활성화/비활성화할 Timer 오브젝트
    public TextMeshProUGUI timerText; // 타이머 텍스트

    private XRBaseInteractable interactable;
    private bool isMapActive = false; // 현재 지도 활성화 상태
    private float timeRemaining = 480f; // 5분 (300초) //8분으로 수정
    private bool timerRunning = false;

    private AudioSource audioSource;
    public AudioClip press;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트가 없으면 추가
        }
    }
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }

        // MapPicture 오브젝트의 초기 활성화 상태를 확인합니다.
        if (mapPicture != null)
        {
            isMapActive = mapPicture.activeSelf;
        }

        // Timer 오브젝트의 초기 활성화 상태를 설정합니다.
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }

        // 타이머 텍스트 초기화
        if (timerText != null)
        {
            UpdateTimerText();
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isMapActive = !isMapActive;
        if (mapPicture != null)
        {
            mapPicture.SetActive(isMapActive);
        }

        if (isMapActive)
        {
            // 지도와 함께 타이머 활성화
            if (timerObject != null)
            {
                timerObject.SetActive(true);
            }
            if (!timerRunning)
            {
                StartCoroutine(TimerCoroutine());
            }
        }
        else
        {
            // 지도와 함께 타이머 비활성화
            if (timerObject != null)
            {
                timerObject.SetActive(false);
            }
        }

        Debug.Log("Button Pressed. Map active state: " + isMapActive);
        //효과음
        PlaySound(press);
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    private IEnumerator TimerCoroutine()
    {
        timerRunning = true;
        while (timeRemaining > 0)
        {
            timeRemaining -= 1f;
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
        }
        timerRunning = false;
        Debug.Log("Timer finished.");
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 현재 남은 시간을 반환하는 메서드
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
