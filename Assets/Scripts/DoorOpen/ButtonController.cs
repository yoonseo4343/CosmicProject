using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController doorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // PlayerHand 태그는 왼쪽 VR 컨트롤러에 붙여야 함
        {
            doorController.OpenDoor();
        }
    }
}
