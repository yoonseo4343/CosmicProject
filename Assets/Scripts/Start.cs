using UnityEngine;

public class CameraInitializer : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 initialRotation;

    void Start()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.Euler(initialRotation);
    }
}
