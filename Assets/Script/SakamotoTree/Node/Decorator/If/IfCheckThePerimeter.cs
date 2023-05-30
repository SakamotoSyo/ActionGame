using System;
using UnityEngine;

public class IfCheckThePerimeter : DecoratorNode
{
    [Header("標的を感知する距離")]
    [SerializeField] private float _sensingDistance;
    [Header("近づいたときのステート")]
    [SerializeField] private State _approachingState;
    [Header("離れたときのステート")]
    [SerializeField] private State _distanceState;
    protected override void OnExit(Environment env)
    {
       
    }

    protected override void OnStart(Environment env)
    {

    }

    protected override State OnUpdate(Environment env)
    {
        var dist = (env.mySelf.transform.position - env.target.transform.position).sqrMagnitude;
        if (dist < _sensingDistance)
        {
            return _approachingState;
        }

        return _distanceState;
    }
}