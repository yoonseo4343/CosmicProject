using UnityEngine;
using System.Collections;

public class VentController : MonoBehaviour
{
    public Transform teleportTarget1;  // �ڷ���Ʈ ��ġ 1
    public Transform teleportTarget2;  // �ڷ���Ʈ ��ġ 2
    public Transform teleportTarget3;  // �ڷ���Ʈ ��ġ 3
    public float openDuration = 1.0f;  // ������ �ð�
    public float closeDuration = 1.0f; // ������ �ð�
    private bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 openPosition;
    private Quaternion initialRotation;
    private Quaternion openRotation;
    private Transform selectedTeleportTarget;

    void Start()
    {
        // ��Ʈ�� �ʱ� ��ġ�� ȸ������ ����
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // ��Ʈ�� ���� ��ġ�� ȸ������ ����
        openPosition = new Vector3(initialPosition.x, initialPosition.y + 0.8f, initialPosition.z + 1.0f);
        openRotation = Quaternion.Euler(initialRotation.eulerAngles.x + 90, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        // ��Ʈ�� �ʱ� ��ġ�� ȸ�������� ����
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    public void SelectLocation(Transform target)
    {
        selectedTeleportTarget = target;
        if (!isOpen)
        {
            StartCoroutine(OpenVent());
        }
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
        Transform target = selectedTeleportTarget;

        if (target == null)
        {
            // �������� ��ġ ����
            int randomIndex = Random.Range(0, 3);
            switch (randomIndex)
            {
                case 0:
                    target = teleportTarget1;
                    break;
                case 1:
                    target = teleportTarget2;
                    break;
                case 2:
                    target = teleportTarget3;
                    break;
            }
        }

        GameObject player = GameObject.FindWithTag("Player");  // �÷��̾� �±׷� �÷��̾� ������Ʈ ã��
        if (player != null && target != null)
        {
            Debug.Log("Teleporting player to target position");
            player.transform.position = target.position;

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
