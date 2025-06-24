using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleRotation : MonoBehaviour
{
    public Transform pivotPoint; // �Ǻ� ����Ʈ�� ����Ű�� Transform
    private XRGrabInteractable grabInteractable;
    private Transform interactorTransform;
    private Vector3 initialInteractorPosition;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        interactorTransform = args.interactorObject.transform;
        initialInteractorPosition = interactorTransform.position;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        interactorTransform = null;
    }

    void Update()
    {
        if (interactorTransform != null)
        {
            RotateHandle();
        }
    }

    void RotateHandle()
    {
        Vector3 currentInteractorPosition = interactorTransform.position;
        Vector3 direction = currentInteractorPosition - initialInteractorPosition;

        // ȸ���� ��� (���⼭�� y�� ȸ���� ���)
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

        // �������� �߽����� ȸ�� ����
        transform.RotateAround(pivotPoint.position, Vector3.up, angle);

        // �ʱⰪ ������Ʈ
        initialInteractorPosition = currentInteractorPosition;
    }
}
