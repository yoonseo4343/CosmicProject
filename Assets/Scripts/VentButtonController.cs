using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentButtonController : MonoBehaviour
{
    public VentController ventController;
    public int buttonIndex;

    void OnMouseDown()
    {
        switch (buttonIndex)
        {
            case 1:
                ventController.SelectLocation(ventController.teleportTarget1);
                break;
            case 2:
                ventController.SelectLocation(ventController.teleportTarget2);
                break;
            case 3:
                ventController.SelectLocation(ventController.teleportTarget3);
                break;
            default:
                Debug.LogError("Invalid button index");
                break;
        }
    }
}
