using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNode : ActionNode
{
    [SerializeField] private int _moveSpeed;
    [Header("‚Ç‚Ì’ö“x‚Ü‚Å‹ß‚Ã‚¢‚½‚ç“®‚­‚Ì‚ð‚â‚ß‚é‚©")]
    [SerializeField] private float _rangeNum;
    [System.NonSerialized] private NavMeshAgent _agent;
    protected override void OnExit(Environment env)
    {
        env.MySelfAnim.SetBool("Move", false);
        _agent.SetDestination(env.mySelf.transform.position);
    }

    protected override void OnStart(Environment env)
    {
        _agent = env.mySelf.GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
    }

    protected override State OnUpdate(Environment env)
    {
        var dist = (env.mySelf.transform.position - env.target.transform.position).sqrMagnitude;
        if (dist < _rangeNum)
        {
            return State.Success;
        }
        env.ConditionAnim.SetTrigger("Detection");
        env.MySelfAnim.SetBool("Move", true);
        _agent.SetDestination(env.target.transform.position);
        return State.Running;
    }
}
