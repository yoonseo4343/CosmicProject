using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // �浹 �̺�Ʈ�� �߻����� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // PlayerHand �±״� ���� VR ��Ʈ�ѷ��� �ٿ��� ��
        {
            Debug.Log("XR Rig has entered the trigger zone!");

        }
    }
}
