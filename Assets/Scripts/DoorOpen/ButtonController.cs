using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController doorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // PlayerHand �±״� ���� VR ��Ʈ�ѷ��� �ٿ��� ��
        {
            doorController.OpenDoor();
        }
    }
}
