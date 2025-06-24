using UnityEngine;

public class Trash : MonoBehaviour
{
    private BroomManager broomManager;

    private void Start()
    {
        broomManager = FindObjectOfType<BroomManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashBin"))
        {
            gameObject.SetActive(false);
            broomManager.CheckMissionStatus(); // �����Ⱑ ��Ȱ��ȭ�� ������ ���¸� Ȯ���մϴ�.
        }
    }
}
