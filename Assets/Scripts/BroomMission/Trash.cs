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
            broomManager.CheckMissionStatus(); // 쓰레기가 비활성화될 때마다 상태를 확인합니다.
        }
    }
}
