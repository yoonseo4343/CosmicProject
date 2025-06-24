using UnityEngine;

public class WireManager : MonoBehaviour
{
    public GameObject RedWire;
    public GameObject GreenWire;
    public GameObject BlueWire;

    public GameObject missionF;
    public GameObject missionT;

    public string missionName = "WireMission";
    public MissionManager missionManager; // Inspector���� ������ �� �ֵ��� public���� ����


    private AudioSource audioSource;
    public AudioClip success;

    private bool missionCompleted = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }
    }
    void Update()
    {
        CheckMissionCompletion();
    }

    void CheckMissionCompletion()
    {
        if (!missionCompleted && RedWire.activeSelf && GreenWire.activeSelf && BlueWire.activeSelf)
        {
            CompleteMission();
        }
    }

    void CompleteMission()
    {
        missionCompleted = true;
        Debug.Log("�輱 ���� �̼� ����");
        // ���⿡ �̼� �Ϸ� �� ������ �߰� �۾��� �ۼ��ϼ���.
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
