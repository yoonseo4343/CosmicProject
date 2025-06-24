using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    public GameObject objectA; // 활성화할 오브젝트
    public ActionBasedController controller; // ActionBasedController 사용

    void Start()
    {
        if (objectA != null)
        {
            objectA.SetActive(false); // 처음에는 비활성화
        }
    }

    void Update()
    {
        if (controller != null && controller.activateAction.action.ReadValue<float>() > 0.1f)
        {
            if (objectA != null)
            {
                objectA.SetActive(true); // 트리거 버튼을 누르면 오브젝트 활성화
            }
        }
    }
}
