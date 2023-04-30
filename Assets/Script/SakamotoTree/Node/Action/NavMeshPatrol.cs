using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NavMeshPatrol : ActionNode
{
    [Header("œpœj‚·‚é”ÍˆÍ")]
    [SerializeField] private float _patrolRange = 0;
    [NonSerialized] private Vector3 _startPosition;
    [NonSerialized] private Vector3 _goalPosition;
    [NonSerialized] private NavMeshAgent _agent;

    protected override void OnExit(Environment env)
    {

    }

    protected override void OnStart(Environment env)
    {
        _startPosition = env.mySelf.transform.position;
        _agent = env.mySelf.GetComponent<NavMeshAgent>();
        SelectPosition();
    }

    protected override State OnUpdate(Environment env)
    {
        if (env.mySelf.transform.position.x == _goalPosition.x && env.mySelf.transform.position.z == _goalPosition.z) 
        {
            SelectPosition();
            Debug.Log("ƒŠƒZƒbƒg");
        }
       
        Debug.Log("“®‚¢‚Ä‚Ü‚·");
        _agent.SetDestination(_goalPosition);
        return State.Running;
    }

    public void SelectPosition() 
    {
        _goalPosition.x = Random.Range(_startPosition.x - _patrolRange, _startPosition.x + _patrolRange);
        _goalPosition.y = _startPosition.y;
        _goalPosition.z = Random.Range(_startPosition.z - _patrolRange, _startPosition.z + _patrolRange);
    }
}