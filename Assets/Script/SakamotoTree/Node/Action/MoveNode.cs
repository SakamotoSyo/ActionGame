using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNode : ActionNode
{
    [SerializeField] private int _moveSpeed;
    private NavMeshAgent _agent;
    protected override void OnExit(Environment env)
    {
        
    }

    protected override void OnStart(Environment env)
    {
       _agent = env.mySelf.GetComponent<NavMeshAgent>();
    }

    protected override State OnUpdate(Environment env)
    {
        _agent.SetDestination(env.target.transform.position);
        _agent.speed = _moveSpeed;
        Debug.Log("íTçıíÜ");
        return State.Running;
    }
}
