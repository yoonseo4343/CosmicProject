using UnityEngine;
using System.Collections;

public class VenttoHomeleft : MonoBehaviour
{
    public Vector3 teleportTarget = new Vector3(-21.48f, 2.507f, 10.65f);  // �ڷ���Ʈ ��ġ
    public float openDuration = 1.0f;  // ������ �ð�
    public float closeDuration = 1.0f; // ������ �ð�
    private bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 openPosition;
    private Quaternion initialRotation;
    private Quaternion openRotation;

    void Start()
    {
        // ��Ʈ�� �ʱ� ��ġ�� ȸ������ ���� ��ġ�� ȸ�������� ����
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // ��Ʈ�� ���� ��ġ�� ȸ������ ����Ͽ� ����
        openPosition = new Vector3(initialPosition.x - 1.0f, initialPosition.y + 0.8f, initialPosition.z);
        openRotation = Quaternion.Euler(initialRotation.eulerAngles.x + 90, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        // ��Ʈ�� �ʱ� ��ġ�� ȸ������ ��� (����׿�)
        Debug.Log("Initial Position: " + initialPosition);
        Debug.Log("Initial Rotation: " + initialRotation.eulerAngles);
        Debug.Log("Open Position: " + openPosition);
        Debug.Log("Open Rotation: " + openRotation.eulerAngles);
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenVent());
        }
    }

    IEnumerator OpenVent()
    {
        isOpen = true;
        float elapsedTime = 0;

        // ��Ʈ�� ������ �ִϸ��̼��� ��ũ��Ʈ�� ����
        while (elapsedTime < openDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, openPosition, elapsedTime / openDuration);
            transform.rotation = Quaternion.Lerp(initialRotation, openRotation, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ�� ȸ�������� ����
        transform.position = openPosition;
        transform.rotation = openRotation;

        // �ڷ���Ʈ ��� ����
        Teleport();
    }

    void Teleport()
    {
        GameObject player = GameObject.FindWithTag("Player");  // �÷��̾� �±׷� �÷��̾� ������Ʈ ã��
        if (player != null)
        {
            Debug.Log("Teleporting player to target position");
            player.transform.position = teleportTarget;

            // �ڷ���Ʈ �� ��Ʈ �ݱ�
            StartCoroutine(CloseVent());
        }
        else
        {
            Debug.LogError("Player object not found or tag is incorrect");
        }
    }

    IEnumerator CloseVent()
    {
        float elapsedTime = 0;

        // ��Ʈ�� ������ �ִϸ��̼��� ��ũ��Ʈ�� ����
        while (elapsedTime < closeDuration)
        {
            transform.position = Vector3.Lerp(openPosition, initialPosition, elapsedTime / closeDuration);
            transform.rotation = Quaternion.Lerp(openRotation, initialRotation, elapsedTime / closeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ�� ȸ�������� ����
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isOpen = false;
    }
}
