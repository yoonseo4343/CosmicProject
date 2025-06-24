using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // 충돌 이벤트가 발생했을 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // PlayerHand 태그는 왼쪽 VR 컨트롤러에 붙여야 함
        {
            Debug.Log("XR Rig has entered the trigger zone!");

        }
    }
}
