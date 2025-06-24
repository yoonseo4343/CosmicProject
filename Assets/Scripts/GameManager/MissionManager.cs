using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Dictionary를 사용하기 위해 필요한 네임스페이스
using UnityEngine.UI; // UI 관련 코드 추가
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    private Dictionary<string, bool> missionStatus = new Dictionary<string, bool>()
    {
        { "WireMission", false },
        { "WaterMission", false },
        { "DestroyMission", false },
        { "BroomMission", false }
    };

    public delegate void AllMissionsCompletedDelegate();
    public event AllMissionsCompletedDelegate OnAllMissionsCompleted;

    public Emergency emergencyScript; // Emergency 스크립트를 참조하기 위한 변수
    public TimeReturn timeReturn; // 게임 타이머 불러오기
    public GameObject successLight;
    public DoorController doorController;
    public TriggerZoneHandler triggerZoneHandler;
    public GameObject triggerZone;

    public GameObject timeoutImage; // 시간 초과 이미지
    public GameObject player; // 플레이어 객체

    public GameObject monitor3DAsset1; // 첫 번째 모니터 3D 에셋
    public GameObject monitor3DAsset2; // 두 번째 모니터 3D 에셋
    public Texture2D gameClearImage; // 게임 클리어 시 변경될 이미지
    public Texture2D postCollisionImage; // 충돌 후 변경될 이미지

    private bool isMissionCompleteImageSet = false;
    private bool hasSwitchedImage = false;

    private void Start()
    {
        if (emergencyScript == null)
        {
            Debug.LogError("Emergency 스크립트가 설정되지 않았습니다.");
        }

        if (timeoutImage != null)
        {
            timeoutImage.SetActive(false); // 시간 초과 이미지를 처음에 비활성화
        }

        if (monitor3DAsset1 != null && monitor3DAsset2 != null)
        {
            monitor3DAsset1.SetActive(true);  // 첫 번째 모니터 활성화
            monitor3DAsset2.SetActive(false); // 두 번째 모니터 비활성화
        }

        // if (monitorImage != null)
        // {
        //     monitorImage.GetComponent<Button>().onClick.AddListener(OnMonitorImageClick);
        // }
    }

    private void Update()
    {
        if (timeReturn != null)
        {
            float remainingTime = timeReturn.GetRemainingTime();
            if (remainingTime == 0.0f)
            {
                TimeOver();
            }
        }
    }

    public void SetMissionStatus(string missionName, bool isSuccess)
    {
        if (missionStatus.ContainsKey(missionName))
        {
            missionStatus[missionName] = isSuccess;

            // 성공한 미션의 수가 2개인지 확인
            int successfulMissionsCount = GetSuccessfulMissionsCount();
            Debug.Log("Successful Missions Count: " + successfulMissionsCount); // 디버그 메시지 추가

            if (successfulMissionsCount == 2)
            {
                if (emergencyScript != null)
                {
                    StartCoroutine(SetEmergencyCheckAfterDelay(3.0f)); // 3초 후에 emergencyCheck 설정
                }
                else
                {
                    Debug.LogError("Emergency 스크립트가 null입니다.");
                }
            }

            // 모든 미션이 성공적으로 완료되었는지 확인
            if (AreAllMissionsSuccessful())
            {
                AllMissionsCompleted();
            }
        }
        else
        {
            Debug.LogWarning("Invalid mission name: " + missionName);
        }
    }

    public bool IsMissionSuccessful(string missionName)
    {
        if (missionStatus.ContainsKey(missionName))
        {
            return missionStatus[missionName];
        }
        return false;
    }

    public bool AreAllMissionsSuccessful()
    {
        foreach (bool status in missionStatus.Values)
        {
            if (!status)
                return false;
        }
        return true;
    }

    private void AllMissionsCompleted() // 미션 모두 완료 시 문 열리는 애니메이션 재생 + success light 활성화 시키기
    {
        if (OnAllMissionsCompleted != null)
        {
            OnAllMissionsCompleted();
        }

        // 성공 라이트를 활성화
        if (successLight != null)
        {
            successLight.SetActive(true);
            Debug.Log("All missions completed. Success light activated.");
            // 문 열기
            doorController.SuccessDoor();
            // 트리거존 활성화
            triggerZone.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Success light is not assigned.");
        }
    }

    private int GetSuccessfulMissionsCount()
    {
        int count = 0;
        foreach (bool status in missionStatus.Values)
        {
            if (status)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerator SetEmergencyCheckAfterDelay(float delay) // 3초 대기 후 이머전시 업데이트
    {
        yield return new WaitForSeconds(delay);
        emergencyScript.emergencyCheck = true;
        emergencyScript.checkEmergency(); // 이머전시 상태를 업데이트
        Debug.Log("Emergency Check set to true after delay."); // 디버그 메시지 추가
    }

    private void TimeOver() // 시간 초과 엔딩
    {
        Debug.Log("Game Time Countdown has ended.");
        //if (timeoutImage != null)
        //{
        //    timeoutImage.SetActive(true); // 시간 초과 이미지를 활성화
        //}
        SceneManager.LoadScene("EmergencyEnding");
    }

    public void GameClear()
    {
        Debug.Log("Game Clear initiated.");

        // 첫 번째 모니터 비활성화, 두 번째 모니터 활성화
        if (monitor3DAsset1 != null && monitor3DAsset2 != null)
        {
            monitor3DAsset1.SetActive(false);
            monitor3DAsset2.SetActive(true);

            // 두 번째 모니터의 이미지를 gameClearImage로 변경
            Renderer monitorRenderer = monitor3DAsset2.GetComponent<Renderer>();
            if (monitorRenderer != null && gameClearImage != null)
            {
                monitorRenderer.material.mainTexture = gameClearImage;
                Debug.Log("Second monitor image switched to game clear image.");
            }
        }

        // PlayerHand 태그와 충돌 시 이미지를 변경하는 트리거 추가
        GameObject monitorSwitchTrigger = new GameObject("MonitorSwitchTrigger");
        monitorSwitchTrigger.transform.position = monitor3DAsset2.transform.position;
        monitorSwitchTrigger.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        monitorSwitchTrigger.AddComponent<BoxCollider>().isTrigger = true;
        monitorSwitchTrigger.AddComponent<MonitorSwitchTrigger>().Initialize(this);
    }

    public void SwitchMonitorImage()
    {
        if (!hasSwitchedImage && monitor3DAsset2 != null)
        {
            Renderer monitorRenderer = monitor3DAsset2.GetComponent<Renderer>();
            if (monitorRenderer != null && postCollisionImage != null)
            {
                monitorRenderer.material.mainTexture = postCollisionImage;
                hasSwitchedImage = true;
                Debug.Log("Second monitor image switched to post collision image.");

                // 충돌 후 5초 뒤 플레이어의 시야를 180도 돌리는 코루틴 시작
                StartCoroutine(RotatePlayerAfterDelay());
            }
        }
    }

    private IEnumerator RotatePlayerAfterDelay()
    {
        yield return new WaitForSeconds(5.0f);
        player.transform.Rotate(0, 180, 0);
        Debug.Log("Player rotated 180 degrees.");
    }
}

public class MonitorSwitchTrigger : MonoBehaviour
{
    private MissionManager missionManager;

    public void Initialize(MissionManager manager)
    {
        missionManager = manager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            Debug.Log("PlayerHand detected, switching monitor image.");
            missionManager.SwitchMonitorImage();
            Destroy(gameObject); // 트리거를 제거하여 중복 호출 방지
        }
    }
}
