using UnityEngine;

public class WireManager : MonoBehaviour
{
    public GameObject RedWire;
    public GameObject GreenWire;
    public GameObject BlueWire;

    public GameObject missionF;
    public GameObject missionT;

    public string missionName = "WireMission";
    public MissionManager missionManager; // Inspector에서 설정할 수 있도록 public으로 선언


    private AudioSource audioSource;
    public AudioClip success;

    private bool missionCompleted = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트가 없으면 추가
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
        Debug.Log("배선 수리 미션 성공");
        // 여기에 미션 완료 시 수행할 추가 작업을 작성하세요.
        missionF.SetActive(false);
        missionT.SetActive(true);

        if (missionManager != null)
        {
            missionManager.SetMissionStatus(missionName, true);
            PlaySound(success);
        }
        else
        {
            Debug.LogError("MissionManager가 null입니다.");
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
