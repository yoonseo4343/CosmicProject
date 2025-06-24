using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    public Transform[] waypoints; // 경로의 웨이포인트들
    public float speed = 2f; // 오브젝트 이동 속도
    private int currentWaypointIndex = 0; // 현재 목표 웨이포인트 인덱스

    void Update()
    {
        // 현재 목표 웨이포인트를 얻는다
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 현재 위치에서 목표 웨이포인트로의 방향을 계산한다
        Vector3 direction = targetWaypoint.position - transform.position;

        // 방향으로 오브젝트를 이동시킨다
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // 오브젝트가 목표 웨이포인트에 거의 도달했는지 확인한다
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // 오브젝트를 정확하게 목표 웨이포인트 위치로 이동시킨다
            transform.position = targetWaypoint.position;

            // 다음 웨이포인트로 이동
            currentWaypointIndex++;

            // 모든 웨이포인트를 다 돌았으면 다시 첫 번째 웨이포인트로 돌아간다
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
