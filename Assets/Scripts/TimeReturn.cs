using UnityEngine;

public class TimeReturn : MonoBehaviour
{
    public ButtonPressHandler buttonPressHandler;

    void Start()
    {
        if (buttonPressHandler == null)
        {
            Debug.LogError("ButtonPressHandler reference is not set.");
        }
    }

    public float GetRemainingTime() //���� �ð� ��ȯ �Լ�
    {
        if (buttonPressHandler != null)
        {
            return buttonPressHandler.GetTimeRemaining();
        }
        else
        {
            Debug.LogError("ButtonPressHandler reference is not set.");
            return 0f;
        }
    }
}
