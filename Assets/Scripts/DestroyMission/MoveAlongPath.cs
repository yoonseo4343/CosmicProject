using UnityEngine;

public class MoveAlongPath : MonoBehaviour
{
    public Transform[] waypoints; // ����� ��������Ʈ��
    public float speed = 2f; // ������Ʈ �̵� �ӵ�
    private int currentWaypointIndex = 0; // ���� ��ǥ ��������Ʈ �ε���

    void Update()
    {
        // ���� ��ǥ ��������Ʈ�� ��´�
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // ���� ��ġ���� ��ǥ ��������Ʈ���� ������ ����Ѵ�
        Vector3 direction = targetWaypoint.position - transform.position;

        // �������� ������Ʈ�� �̵���Ų��
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // ������Ʈ�� ��ǥ ��������Ʈ�� ���� �����ߴ��� Ȯ���Ѵ�
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // ������Ʈ�� ��Ȯ�ϰ� ��ǥ ��������Ʈ ��ġ�� �̵���Ų��
            transform.position = targetWaypoint.position;

            // ���� ��������Ʈ�� �̵�
            currentWaypointIndex++;

            // ��� ��������Ʈ�� �� �������� �ٽ� ù ��° ��������Ʈ�� ���ư���
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
