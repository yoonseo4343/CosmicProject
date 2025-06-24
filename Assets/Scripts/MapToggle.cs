using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public GameObject mapPrefab; // ���� ������
    private GameObject mapInstance; // ���� �ν��Ͻ�
    private bool isMapVisible = false; // ������ ���� ����

    void Start()
    {
        // ���� �ν��Ͻ��� ��Ȱ��ȭ�� ���·� ����
        mapInstance = Instantiate(mapPrefab);
        mapInstance.SetActive(false);
    }

    void OnMouseDown()
    {
        ToggleMap();
    }

    void ToggleMap()
    {
        isMapVisible = !isMapVisible; // ���� ���
        mapInstance.SetActive(isMapVisible); // ���� Ȱ��ȭ/��Ȱ��ȭ
    }
}

