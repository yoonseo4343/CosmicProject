using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Dictionary�� ����ϱ� ���� �ʿ��� ���ӽ����̽�
using UnityEngine.UI; // UI ���� �ڵ� �߰�
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

    public Emergency emergencyScript; // Emergency ��ũ��Ʈ�� �����ϱ� ���� ����
    public TimeReturn timeReturn; // ���� Ÿ�̸� �ҷ�����
    public GameObject successLight;
    public DoorController doorController;
    public TriggerZoneHandler triggerZoneHandler;
    public GameObject triggerZone;

    public GameObject timeoutImage; // �ð� �ʰ� �̹���
    public GameObject player; // �÷��̾� ��ü

    public GameObject monitor3DAsset1; // ù ��° ����� 3D ����
    public GameObject monitor3DAsset2; // �� ��° ����� 3D ����
    public Texture2D gameClearImage; // ���� Ŭ���� �� ����� �̹���
    public Texture2D postCollisionImage; // �浹 �� ����� �̹���

    private bool isMissionCompleteImageSet = false;
    private bool hasSwitchedImage = false;

    private void Start()
    {
        if (emergencyScript == null)
        {
            Debug.LogError("Emergency ��ũ��Ʈ�� �������� �ʾҽ��ϴ�.");
        }

        if (timeoutImage != null)
        {
            timeoutImage.SetActive(false); // �ð� �ʰ� �̹����� ó���� ��Ȱ��ȭ
        }

        if (monitor3DAsset1 != null && monitor3DAsset2 != null)
        {
            monitor3DAsset1.SetActive(true);  // ù ��° ����� Ȱ��ȭ
            monitor3DAsset2.SetActive(false); // �� ��° ����� ��Ȱ��ȭ
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

            // ������ �̼��� ���� 2������ Ȯ��
            int successfulMissionsCount = GetSuccessfulMissionsCount();
            Debug.Log("Successful Missions Count: " + successfulMissionsCount); // ����� �޽��� �߰�

            if (successfulMissionsCount == 2)
            {
                if (emergencyScript != null)
                {
                    StartCoroutine(SetEmergencyCheckAfterDelay(3.0f)); // 3�� �Ŀ� emergencyCheck ����
                }
                else
                {
                    Debug.LogError("Emergency ��ũ��Ʈ�� null�Դϴ�.");
                }
            }

            // ��� �̼��� ���������� �Ϸ�Ǿ����� Ȯ��
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

    private void AllMissionsCompleted() // �̼� ��� �Ϸ� �� �� ������ �ִϸ��̼� ��� + success light Ȱ��ȭ ��Ű��
    {
        if (OnAllMissionsCompleted != null)
        {
            OnAllMissionsCompleted();
        }

        // ���� ����Ʈ�� Ȱ��ȭ
        if (successLight != null)
        {
            successLight.SetActive(true);
            Debug.Log("All missions completed. Success light activated.");
            // �� ����
            doorController.SuccessDoor();
            // Ʈ������ Ȱ��ȭ
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

    private IEnumerator SetEmergencyCheckAfterDelay(float delay) // 3�� ��� �� �̸����� ������Ʈ
    {
        yield return new WaitForSeconds(delay);
        emergencyScript.emergencyCheck = true;
        emergencyScript.checkEmergency(); // �̸����� ���¸� ������Ʈ
        Debug.Log("Emergency Check set to true after delay."); // ����� �޽��� �߰�
    }

    private void TimeOver() // �ð� �ʰ� ����
    {
        Debug.Log("Game Time Countdown has ended.");
        //if (timeoutImage != null)
        //{
        //    timeoutImage.SetActive(true); // �ð� �ʰ� �̹����� Ȱ��ȭ
        //}
        SceneManager.LoadScene("EmergencyEnding");
    }

    public void GameClear()
    {
        Debug.Log("Game Clear initiated.");

        // ù ��° ����� ��Ȱ��ȭ, �� ��° ����� Ȱ��ȭ
        if (monitor3DAsset1 != null && monitor3DAsset2 != null)
        {
            monitor3DAsset1.SetActive(false);
            monitor3DAsset2.SetActive(true);

            // �� ��° ������� �̹����� gameClearImage�� ����
            Renderer monitorRenderer = monitor3DAsset2.GetComponent<Renderer>();
            if (monitorRenderer != null && gameClearImage != null)
            {
                monitorRenderer.material.mainTexture = gameClearImage;
                Debug.Log("Second monitor image switched to game clear image.");
            }
        }

        // PlayerHand �±׿� �浹 �� �̹����� �����ϴ� Ʈ���� �߰�
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

                // �浹 �� 5�� �� �÷��̾��� �þ߸� 180�� ������ �ڷ�ƾ ����
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
            Destroy(gameObject); // Ʈ���Ÿ� �����Ͽ� �ߺ� ȣ�� ����
        }
    }
}
