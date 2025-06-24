using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction; // XRLever Ŭ������ ���ӽ����̽� �߰�
using TMPro; // TextMeshProUGUI�� ����ϱ� ���� ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;

//��ũ��Ʈ ��Ȱ��ȭ ���� �߰� �Ǿ�����
public class Emergency : MonoBehaviour
{
    public bool emergencyCheck = false; // ����Ʈ false

    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;
    public GameObject light5;
    public GameObject panel;

    public GameObject leverObject; // Lever ������Ʈ�� �����ϱ� ���� ����
    private XRLever lever;
    private float currentAngle;

    public MissionManager missionManager; // Inspector���� ����

    public GameObject emergencyImage; // �ð� �ʰ� �̹���
    public GameObject timerUI; // Ÿ�̸� UI ������Ʈ
    public TextMeshProUGUI timerText; // Ÿ�̸� �ؽ�Ʈ

    private float countdownTime = 80f; // 80�� Ÿ�̸�
    private bool isCountingDown = false;

    private AudioSource audioSource;
    public AudioClip emergencySound;

    // �߰��� ����
    private string emergencyEndingSceneName = "EmergencyEnding"; // �̵��� ���� �̸� �Ǵ� �ε���

    private void Start()
    {
        if (leverObject != null)
        {
            lever = leverObject.GetComponent<XRLever>();
        }

        if (missionManager == null)
        {
            Debug.LogError("MissionManager�� �������� �ʾҽ��ϴ�.");
        }

        if (timerUI == null || timerText == null)
        {
            Debug.LogError("Timer UI�� �������� �ʾҽ��ϴ�.");
        }

        timerUI.SetActive(false); // Ÿ�̸� UI�� ó������ ��Ȱ��ȭ

        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ ��������
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource ������Ʈ�� ������ �߰�
        }

        audioSource.volume = 0.7f; //���� ���� ����
        checkEmergency();
    }

    private void Update()
    {
        if (lever != null)
        {
            currentAngle = lever.lookAngle;
            checkLever();

            // currentAngle�� -50 �����̸� ���� �ڵ� ������ ����
            if (!emergencyCheck)
            {
                return;
            }
        }
    }

    public void checkEmergency()
    {
        if (emergencyCheck) // �̸����� ��Ȳ ��
        {
            light1.SetActive(true);
            light2.SetActive(true);
            light3.SetActive(true);
            light4.SetActive(true);
            light5.SetActive(true);
            panel.SetActive(false);
            timerUI.SetActive(true);
            PlaySound(emergencySound);
            if (!isCountingDown)
            {
                StartCoroutine(StartCountdown());
            }
            Debug.Log("Emergency lights on."); // ����� �޽��� �߰�
        }
        else // �̸����� �̼� �Ϸ� �� ���� ����
        {
            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(false);
            light4.SetActive(false);
            light5.SetActive(false);
            timerUI.SetActive(false); // Ÿ�̸� UI ��Ȱ��ȭ
            StopSound(); // �̸����� ��Ȳ�� �ƴϸ� �Ҹ� ��Ȱ��ȭ
            isCountingDown = false; // ī��Ʈ�ٿ� ���� ����
            Debug.Log("Emergency lights off."); // ����� �޽��� �߰�
        }
    }

    private IEnumerator StartCountdown()
    {
        timerUI.SetActive(true); // Ÿ�̸� UI Ȱ��ȭ
        isCountingDown = true;
        float remainingTime = countdownTime;
        while (remainingTime > 0)
        {
            timerText.text = remainingTime.ToString("F0"); // UI�� ���� �ð� ǥ��
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        timerText.text = "0";
        if (emergencyCheck)
        {
            HandleCountdownEnd(); // 80�ʰ� ������ �̺�Ʈ ó�� �Լ� ȣ��
        }

        timerUI.SetActive(false); // Ÿ�̸� UI ��Ȱ��ȭ
        isCountingDown = false;
    }

    private void HandleCountdownEnd()
    {
        Debug.Log("Emergency Mission Countdown has ended.");
        // �̸����� Ÿ�̸Ӱ� ������ �� ���� �̵�
        SceneManager.LoadScene(emergencyEndingSceneName);
    }

    void checkLever() //�̸����� �̼� �Ϸ�
    {
        if (currentAngle <= -50)
        {
            emergencyCheck = false;
            checkEmergency(); // ���°� ����Ǿ����Ƿ� ���� ���µ� ������Ʈ
            gameObject.GetComponent<Emergency>().enabled = false; //��ũ��Ʈ ��Ȱ��ȭ
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true; // �ݺ� ��� ����
            audioSource.Play();
        }
    }

    private void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // �Ҹ� ��Ȱ��ȭ
        }
    }
}
