using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Repeater : DecoratorNode
{
    [SerializeField] private int _time = 0;
    [NonSerialized] private float _countTime = 0;

    protected override void OnExit(Environment env)
    {
        
    }

    protected override void OnStart(Environment env)
    {
       
    }

    protected override State OnUpdate(Environment env)
    {
        _countTime += Time.deltaTime;
        if (_time > _countTime)
        {
            Child.update(env);
            return State.Running;
        }
        else 
        {
            Debug.Log("Ž¸”s‚µ‚Ä‚Ü‚·");
            Child.CurrentState?.Invoke(State.Failure);
            return State.Failure;
        }

        
    }
}
