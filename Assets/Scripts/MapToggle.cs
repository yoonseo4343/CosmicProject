using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public GameObject mapPrefab; // 지도 프리팹
    private GameObject mapInstance; // 지도 인스턴스
    private bool isMapVisible = false; // 지도의 현재 상태

    void Start()
    {
        // 지도 인스턴스를 비활성화된 상태로 생성
        mapInstance = Instantiate(mapPrefab);
        mapInstance.SetActive(false);
    }

    void OnMouseDown()
    {
        ToggleMap();
    }

    void ToggleMap()
    {
        isMapVisible = !isMapVisible; // 상태 토글
        mapInstance.SetActive(isMapVisible); // 지도 활성화/비활성화
    }
}

