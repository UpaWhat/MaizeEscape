using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdAIScript : MonoBehaviour
{

    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private float DetectionDistance;
    [SerializeField] private float KillDistance;
    [SerializeField] private BirdState CurrentState;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    private bool playerFound;

    private enum BirdState
    {
        Idle, Chase, Inactive,
    };

    private void Awake()
    {
        playerFound = false;
        CurrentState = BirdState.Inactive;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        checkPlayerDistance();
        
        switch(CurrentState)
        {
            case BirdState.Idle:
                navMeshAgent.destination = movePositionTransform.localToWorldMatrix.GetPosition();
                break;
            case BirdState.Chase:
                navMeshAgent.destination = playerTransform.position;
                break;
            case BirdState.Inactive:
                navMeshAgent.isStopped = true;
                break;
        }
        
    }

    private void checkPlayerDistance()
    {
        float distance = Vector3.Distance(gameObject.transform.position, playerTransform.position);
        if(distance<=DetectionDistance)
        {
            playerFound = true;
            CurrentState = BirdState.Chase;
        }
        if(distance<=KillDistance)
        {
            playerTransform.GetComponent<PlayerMovement>().KillPlayer();
        }
    }

    public void setActive()
    {
        CurrentState = BirdState.Idle;
    }
    public void setInactive()
    {
        CurrentState = BirdState.Inactive;
    }

}
