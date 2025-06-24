using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction; // XRLever 클래스의 네임스페이스 추가
using TMPro; // TextMeshProUGUI를 사용하기 위한 네임스페이스 추가
using UnityEngine.SceneManagement;

//스크립트 비활성화 로직 추가 되어있음
public class Emergency : MonoBehaviour
{
    public bool emergencyCheck = false; // 디폴트 false

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;
    public GameObject panel;

    public GameObject leverObject; // Lever 오브젝트를 참조하기 위한 변수
    private XRLever lever;
    private float currentAngle;

    public MissionManager missionManager; // Inspector에서 설정

    public GameObject emergencyImage; // 시간 초과 이미지
    public GameObject timerUI; // 타이머 UI 오브젝트
    public TextMeshProUGUI timerText; // 타이머 텍스트

    private float countdownTime = 80f; // 80초 타이머
    private bool isCountingDown = false;

    private AudioSource audioSource;
    public AudioClip emergencySound;

    // 추가된 변수
    private string emergencyEndingSceneName = "EmergencyEnding"; // 이동할 씬의 이름 또는 인덱스

    private void Start()
    {
        if (leverObject != null)
        {
            lever = leverObject.GetComponent<XRLever>();
        }

        if (missionManager == null)
        {
            Debug.LogError("MissionManager가 설정되지 않았습니다.");
        }

        if (timerUI == null || timerText == null)
        {
            Debug.LogError("Timer UI가 설정되지 않았습니다.");
        }

        timerUI.SetActive(false); // 타이머 UI를 처음에는 비활성화

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트가 없으면 추가
        }

        audioSource.volume = 0.7f; //사운드 볼륨 조절
        checkEmergency();
    }

    private void Update()
    {
        if (lever != null)
        {
            currentAngle = lever.lookAngle;
            checkLever();

            // currentAngle이 -50 이하이면 이후 코드 실행을 중지
            if (!emergencyCheck)
            {
                return;
            }
        }
    }

    public void checkEmergency()
    {
        if (emergencyCheck) // 이머전시 상황 때
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(true);
            light5.SetActive(true);
            panel.SetActive(false);
            timerUI.SetActive(true);
            PlaySound(emergencySound);
            if (!isCountingDown)
            {
                StartCoroutine(StartCountdown());
            }
            Debug.Log("Emergency lights on."); // 디버그 메시지 추가
        }
        else // 이머전시 미션 완료 후 조명 끄기
        {
            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(false);
            light4.SetActive(false);
            light5.SetActive(false);
            timerUI.SetActive(false); // 타이머 UI 비활성화
            StopSound(); // 이머전시 상황이 아니면 소리 비활성화
            isCountingDown = false; // 카운트다운 상태 리셋
            Debug.Log("Emergency lights off."); // 디버그 메시지 추가
        }
    }

    private IEnumerator StartCountdown()
    {
        timerUI.SetActive(true); // 타이머 UI 활성화
        isCountingDown = true;
        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            timerText.text = remainingTime.ToString("F0"); // UI에 남은 시간 표시
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        timerText.text = "0";
        if (emergencyCheck)
        {
            HandleCountdownEnd(); // 80초가 지나면 이벤트 처리 함수 호출
        }

        timerUI.SetActive(false); // 타이머 UI 비활성화
        isCountingDown = false;
    }

    private void HandleCountdownEnd()
    {
        Debug.Log("Emergency Mission Countdown has ended.");
        // 이머전시 타이머가 끝났을 때 엔딩 이동
        SceneManager.LoadScene(emergencyEndingSceneName);
    }

    void checkLever() //이머전시 미션 완료
    {
        if (currentAngle <= -50)
        {
            emergencyCheck = false;
            checkEmergency(); // 상태가 변경되었으므로 조명 상태도 업데이트
            gameObject.GetComponent<Emergency>().enabled = false; //스크립트 비활성화
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true; // 반복 재생 설정
            audioSource.Play();
        }
    }

    private void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // 소리 비활성화
        }
    }
}
