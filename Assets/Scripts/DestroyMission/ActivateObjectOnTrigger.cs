using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    public GameObject objectA; // Ȱ��ȭ�� ������Ʈ
    public ActionBasedController controller; // ActionBasedController ���

    void Start()
    {
        if (objectA != null)
        {
            objectA.SetActive(false); // ó������ ��Ȱ��ȭ
        }
    }

    void Update()
    {
        if (controller != null && controller.activateAction.action.ReadValue<float>() > 0.1f)
        {
            if (objectA != null)
            {
                objectA.SetActive(true); // Ʈ���� ��ư�� ������ ������Ʈ Ȱ��ȭ
            }
        }
    }
}
