using UnityEngine;

public class OctagonWallBuilder : MonoBehaviour
{
    public GameObject wallPrefab; // 벽 프리팹을 담을 필드
    public float radius = 4f; // 팔각형의 반지름
    public float wallHeight = 4f; // 벽 높이

    void Start()
    {
        BuildOctagonWall();
    }

    void BuildOctagonWall()
    {
        float angleStep = 360f / 8;
        Vector3 center = transform.position;

        for (int i = 0; i < 8; i++)
        {
            float angle = i * angleStep;
            Vector3 position = center + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.Euler(0, -angle + 22.5f, 0)); // 22.5도 보정

            // 벽의 크기를 설정합니다.
            wall.transform.localScale = new Vector3(1, wallHeight, 1);
        }
    }
}
