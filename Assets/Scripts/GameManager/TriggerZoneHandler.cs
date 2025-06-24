using UnityEngine;

public class TriggerZoneHandler : MonoBehaviour
{
    // XR Rig�� �±׸� "XRRig"�� �����ؾ� �մϴ�.
    public string xrTag = "PlayerHand";
    public MissionManager missionManager;

    // Ʈ���ſ� �ٸ� �ݸ����� ���� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        // ���� �ݸ����� �±װ� XR Rig�� �±׿� ��ġ�ϴ��� Ȯ��
        if (other.CompareTag(xrTag))
        {
            // �̺�Ʈ ó�� �ڵ� �ۼ�
            Debug.Log("XR Rig has entered the trigger zone!");
            //���� Ŭ���� �̵�
            missionManager.GameClear();
        }
    }
}
