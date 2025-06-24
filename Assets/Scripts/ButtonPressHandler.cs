using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ButtonPressHandler : MonoBehaviour
{
    public GameObject mapPicture; // Ȱ��ȭ/��Ȱ��ȭ�� MapPicture ������Ʈ
    public GameObject timerObject; // Ȱ��ȭ/��Ȱ��ȭ�� Timer ������Ʈ
    public TextMeshProUGUI timerText; // Ÿ�̸� �ؽ�Ʈ

    private XRBaseInteractable interactable;
    private bool isMapActive = false; // ���� ���� Ȱ��ȭ ����
    private float timeRemaining = 480f; // 5�� (300��) //8������ ����
    private bool timerRunning = false;

    private AudioSource audioSource;
    public AudioClip press;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }
    }
    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelectEntered);
        }

        // MapPicture ������Ʈ�� �ʱ� Ȱ��ȭ ���¸� Ȯ���մϴ�.
        if (mapPicture != null)
        {
            isMapActive = mapPicture.activeSelf;
        }

        // Timer ������Ʈ�� �ʱ� Ȱ��ȭ ���¸� �����մϴ�.
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }

        // Ÿ�̸� �ؽ�Ʈ �ʱ�ȭ
        if (timerText != null)
        {
            UpdateTimerText();
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isMapActive = !isMapActive;
        if (mapPicture != null)
        {
            mapPicture.SetActive(isMapActive);
        }

        if (isMapActive)
        {
            // ������ �Բ� Ÿ�̸� Ȱ��ȭ
            if (timerObject != null)
            {
                timerObject.SetActive(true);
            }
            if (!timerRunning)
            {
                StartCoroutine(TimerCoroutine());
            }
        }
        else
        {
            // ������ �Բ� Ÿ�̸� ��Ȱ��ȭ
            if (timerObject != null)
            {
                timerObject.SetActive(false);
            }
        }

        Debug.Log("Button Pressed. Map active state: " + isMapActive);
        //ȿ����
        PlaySound(press);
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    private IEnumerator TimerCoroutine()
    {
        timerRunning = true;
        while (timeRemaining > 0)
        {
            timeRemaining -= 1f;
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
        }
        timerRunning = false;
        Debug.Log("Timer finished.");
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ���� ���� �ð��� ��ȯ�ϴ� �޼���
    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
