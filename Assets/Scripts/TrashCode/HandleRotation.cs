using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleRotation : MonoBehaviour
{
    public Transform pivotPoint; // 피봇 포인트를 가리키는 Transform
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

        // 회전량 계산 (여기서는 y축 회전을 고려)
        float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

        // 기준점을 중심으로 회전 적용
        transform.RotateAround(pivotPoint.position, Vector3.up, angle);

        // 초기값 업데이트
        initialInteractorPosition = currentInteractorPosition;
    }
}
