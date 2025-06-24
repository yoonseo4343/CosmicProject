using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Broom : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 빗자루는 VR 컨트롤러에 의해 직접 움직여질 것이므로 물리적 움직임은 필요하지 않습니다.
        // 컨트롤러에 부착된 상태에서 움직임이 처리됩니다.
    }
}
