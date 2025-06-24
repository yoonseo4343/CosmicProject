using UnityEngine;

public class WireCollisionHandler : MonoBehaviour
{
    // 필요한 오브젝트를 인스펙터에서 할당할 수 있도록 public으로 선언합니다.
    public GameObject Wire;
    public GameObject WireR;
    public GameObject WireM;
    public GameObject WireL;

    // Start 메소드에서 GreenWire를 비활성화합니다.
    void Start()
    {
        Wire.SetActive(false);
    }

    // 충돌이 발생했을 때 실행되는 메소드입니다.
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 GreenWireR인지 확인합니다.
        if (collision.gameObject == WireR)
        {
            // GreenWireR, GreenWireM, GreenWireL을 비활성화합니다.
            WireR.SetActive(false);
            WireM.SetActive(false);
            WireL.SetActive(false);

            // GreenWire를 활성화합니다.
            Wire.SetActive(true);
        }
    }
}
