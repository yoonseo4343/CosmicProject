using UnityEngine;
using System.Collections;

public class VenttoHomeleft : MonoBehaviour
{
    public Vector3 teleportTarget = new Vector3(-21.48f, 2.507f, 10.65f);  // 텔레포트 위치
    public float openDuration = 1.0f;  // 열리는 시간
    public float closeDuration = 1.0f; // 닫히는 시간
    private bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 openPosition;
    private Quaternion initialRotation;
    private Quaternion openRotation;

    void Start()
    {
        // 벤트의 초기 위치와 회전값을 현재 위치와 회전값으로 설정
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // 벤트의 열린 위치와 회전값을 계산하여 설정
        openPosition = new Vector3(initialPosition.x - 1.0f, initialPosition.y + 0.8f, initialPosition.z);
        openRotation = Quaternion.Euler(initialRotation.eulerAngles.x + 90, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        // 벤트의 초기 위치와 회전값을 출력 (디버그용)
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

        // 벤트가 열리는 애니메이션을 스크립트로 구현
        while (elapsedTime < openDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, openPosition, elapsedTime / openDuration);
            transform.rotation = Quaternion.Lerp(initialRotation, openRotation, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치와 회전값으로 설정
        transform.position = openPosition;
        transform.rotation = openRotation;

        // 텔레포트 기능 실행
        Teleport();
    }

    void Teleport()
    {
        GameObject player = GameObject.FindWithTag("Player");  // 플레이어 태그로 플레이어 오브젝트 찾기
        if (player != null)
        {
            Debug.Log("Teleporting player to target position");
            player.transform.position = teleportTarget;

            // 텔레포트 후 벤트 닫기
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

        // 벤트가 닫히는 애니메이션을 스크립트로 구현
        while (elapsedTime < closeDuration)
        {
            transform.position = Vector3.Lerp(openPosition, initialPosition, elapsedTime / closeDuration);
            transform.rotation = Quaternion.Lerp(openRotation, initialRotation, elapsedTime / closeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치와 회전값으로 설정
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        isOpen = false;
    }
}
