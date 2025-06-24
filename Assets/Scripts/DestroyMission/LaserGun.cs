using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserGun : MonoBehaviour
{
    public Transform laserOrigin;
    public GameObject laserPrefab;
    public float laserSpeed = 50f;
    public float laserLifeTime = 2f;

    private XRGrabInteractable grabInteractable;
    private XRController controller;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        controller = args.interactorObject.transform.GetComponent<XRController>();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        controller = null;
    }

    void Update()
    {
        if (controller != null && controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, laserOrigin.position, laserOrigin.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = laserOrigin.forward * laserSpeed;
        }
        Destroy(laser, laserLifeTime);
    }
}
