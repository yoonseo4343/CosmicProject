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
        // ���ڷ�� VR ��Ʈ�ѷ��� ���� ���� �������� ���̹Ƿ� ������ �������� �ʿ����� �ʽ��ϴ�.
        // ��Ʈ�ѷ��� ������ ���¿��� �������� ó���˴ϴ�.
    }
}
