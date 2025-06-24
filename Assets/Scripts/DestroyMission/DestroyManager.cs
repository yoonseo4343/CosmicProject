using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    public Rock[] rockObjects; // ��� ������ ������Ʈ�� �����մϴ�.
    public GameObject missionF;
    public GameObject missionT;

    public string missionName = "DestroyMission";
    public MissionManager missionManager; // Inspector���� ������ �� �ֵ��� public���� ����

    private AudioSource audioSource;
    public AudioClip success;

    void Start()
    {
        // MissionManager�� Inspector���� �����Ǿ����� Ȯ���մϴ�.
        if (missionManager == null)
        {
            Debug.LogError("MissionManager�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ���� �� ��� rock ������Ʈ�� ��Ȱ��ȭ �������� Ȯ���մϴ�.
        CheckMissionStatus();

        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }
    }

    public void CheckMissionStatus()
    {
        // ��� rock ������Ʈ�� ��Ȱ��ȭ�Ǿ����� Ȯ���մϴ�.
        bool allRockCollected = true;
        foreach (Rock rock in rockObjects)
        {
            if (rock.gameObject.activeSelf)
            {
                allRockCollected = false;
                break;
            }
        }

        if (allRockCollected)
        {
            OnMissionSuccess();
        }
    }

    void OnMissionSuccess()
    {
        // �̼� ���� �� ó���� ������ ���⿡ �ۼ��մϴ�.
        Debug.Log("���༺ �ı��ϱ� �̼� ����.");
        missionF.SetActive(false);
        missionT.SetActive(true);

        if (missionManager != null)
        {
            missionManager.SetMissionStatus(missionName, true);
            PlaySound(success);
        }
        else
        {
            Debug.LogError("MissionManager�� null�Դϴ�.");
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
