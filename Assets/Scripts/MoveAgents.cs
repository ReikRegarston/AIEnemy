using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MoveAgents : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform nowPoint;

    public Transform[] points;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewPath();
    }

    void Update()
    {
        
    }

    public void SetNewPath()
    {
        Transform moveTo = points[Random.Range(0, points.Length)];

        if (nowPoint != null && moveTo.position == nowPoint.position)
        {
            SetNewPath();
            return;
        }
        nowPoint = moveTo;
        agent.SetDestination(nowPoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
            SetNewPath();
    }
}
