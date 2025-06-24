using UnityEngine;
using System.Threading.Tasks;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public MissionManager missionManager;
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public async void OpenDoor()
    {
        await Task.Delay(2000); // 2�� ���
        PlaySound(openSound);
        animator.SetTrigger("Open");
        await Task.Delay(10000); // 10�� ��������
        CloseDoor();
    }

    public void CloseDoor()
    {
        PlaySound(closeSound); //����
        animator.SetTrigger("Close");
    }

    public void SuccessDoor() // �̼� ���� �� �� ����
    {
        PlaySound(openSound); //����
        animator.SetTrigger("Success");
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip); 
        }
    }
}
