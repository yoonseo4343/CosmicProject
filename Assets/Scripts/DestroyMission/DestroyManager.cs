using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    public Rock[] rockObjects; // 모든 쓰레기 오브젝트를 참조합니다.
    public GameObject missionF;
    public GameObject missionT;

    public string missionName = "DestroyMission";
    public MissionManager missionManager; // Inspector에서 설정할 수 있도록 public으로 선언

    private AudioSource audioSource;
    public AudioClip success;

    void Start()
    {
        // MissionManager가 Inspector에서 설정되었는지 확인합니다.
        if (missionManager == null)
        {
            Debug.LogError("MissionManager가 설정되지 않았습니다.");
            return;
        }

        // 게임 시작 시 모든 rock 오브젝트가 비활성화 상태인지 확인합니다.
        CheckMissionStatus();

        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트가 없으면 추가
        }
    }

    public void CheckMissionStatus()
    {
        // 모든 rock 오브젝트가 비활성화되었는지 확인합니다.
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
        // 미션 성공 시 처리할 내용을 여기에 작성합니다.
        Debug.Log("소행성 파괴하기 미션 성공.");
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
