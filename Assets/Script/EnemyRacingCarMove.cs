using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRacingCarMove : MonoBehaviour
{
    public Transform[] waypoints; // Ʈ�� ��λ��� ��������Ʈ��
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    GameManager GM;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (GM.startRace && waypoints.Length > 0)
        {
            if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.Length)
                {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                }
                else
                {
                    // ��� ��������Ʈ�� ������ ���ϴ� ����(��: ��ȯ, ���� ��)�� �߰�
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (agent == null || agent.path == null)
            return;

        // ����� �ڳ� ���� ��������
        Vector3[] pathCorners = agent.path.corners;

        Gizmos.color = Color.red;

        for (int i = 0; i < pathCorners.Length - 1; i++)
        {
            Gizmos.DrawLine(pathCorners[i], pathCorners[i + 1]);

            Gizmos.DrawSphere(pathCorners[i], 0.2f);
        }

        if (pathCorners.Length > 0)
        {
            Gizmos.DrawSphere(pathCorners[pathCorners.Length - 1], 0.2f);
        }
    }
}
