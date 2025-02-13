using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRacingCarMove : MonoBehaviour
{
    public Transform[] waypoints; // 트랙 경로상의 웨이포인트들
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
                    // 모든 웨이포인트를 지나면 원하는 동작(예: 순환, 정지 등)을 추가
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (agent == null || agent.path == null)
            return;

        // 경로의 코너 지점 가져오기
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
