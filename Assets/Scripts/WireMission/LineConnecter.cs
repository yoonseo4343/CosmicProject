using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnecter : MonoBehaviour
{
    public GameObject[] _objs;

    private LineRenderer line;

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();

        // Set the number of positions in the LineRenderer to match the number of objects
        if (_objs != null && _objs.Length > 0)
        {
            line.positionCount = _objs.Length;
        }
        else
        {
            Debug.LogWarning("No objects assigned to _objs array");
        }
    }

    void Update()
    {
        // Ensure the LineRenderer position count matches the _objs array length
        if (line.positionCount != _objs.Length)
        {
            line.positionCount = _objs.Length;
        }

        for (int i = 0; i < _objs.Length; i++)
        {
            line.SetPosition(i, _objs[i].transform.position);
        }
    }
}
