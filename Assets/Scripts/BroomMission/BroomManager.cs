using UnityEngine;

public class BroomManager : MonoBehaviour
{
    public Trash[] trashObjects; // ��� ������ ������Ʈ�� �����մϴ�.
    public GameObject missionF;
    public GameObject missionT;

    public string missionName = "BroomMission";
    public MissionManager missionManager; // Inspector���� ������ �� �ֵ��� public���� ����

    private AudioSource audioSource;
    public AudioClip success;
    void Start()
    {
        // ���� ���� �� ��� ������ ������Ʈ�� ��Ȱ��ȭ �������� Ȯ���մϴ�.
        CheckMissionStatus();

        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }
    }

    public void CheckMissionStatus()
    {
        // ��� ������ ������Ʈ�� ��Ȱ��ȭ�Ǿ����� Ȯ���մϴ�.
        bool allTrashCollected = true;
        foreach (Trash trash in trashObjects)
        {
            if (trash.gameObject.activeSelf)
            {
                allTrashCollected = false;
                break;
            }
        }

        if (allTrashCollected)
        {
            OnMissionSuccess();
        }
    }

    void OnMissionSuccess()
    {
        // �̼� ���� �� ó���� ������ ���⿡ �ۼ��մϴ�.
        Debug.Log("������ ġ��� �̼� ����.");
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
