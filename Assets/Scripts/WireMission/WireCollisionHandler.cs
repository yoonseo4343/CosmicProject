using UnityEngine;

public class WireCollisionHandler : MonoBehaviour
{
    // �ʿ��� ������Ʈ�� �ν����Ϳ��� �Ҵ��� �� �ֵ��� public���� �����մϴ�.
    public GameObject Wire;
    public GameObject WireR;
    public GameObject WireM;
    public GameObject WireL;

    // Start �޼ҵ忡�� GreenWire�� ��Ȱ��ȭ�մϴ�.
    void Start()
    {
        Wire.SetActive(false);
    }

    // �浹�� �߻����� �� ����Ǵ� �޼ҵ��Դϴ�.
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� GreenWireR���� Ȯ���մϴ�.
        if (collision.gameObject == WireR)
        {
            // GreenWireR, GreenWireM, GreenWireL�� ��Ȱ��ȭ�մϴ�.
            WireR.SetActive(false);
            WireM.SetActive(false);
            WireL.SetActive(false);

            // GreenWire�� Ȱ��ȭ�մϴ�.
            Wire.SetActive(true);
        }
    }
}
