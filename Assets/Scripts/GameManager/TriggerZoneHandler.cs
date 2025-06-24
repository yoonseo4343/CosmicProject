using UnityEngine;

public class TriggerZoneHandler : MonoBehaviour
{
    // XR Rig의 태그를 "XRRig"로 설정해야 합니다.
    public string xrTag = "PlayerHand";
    public MissionManager missionManager;

    // 트리거에 다른 콜리더가 들어올 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        // 들어온 콜리더의 태그가 XR Rig의 태그와 일치하는지 확인
        if (other.CompareTag(xrTag))
        {
            // 이벤트 처리 코드 작성
            Debug.Log("XR Rig has entered the trigger zone!");
            //게임 클리어 이동
            missionManager.GameClear();
        }
    }
}
