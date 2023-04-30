using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sequence : ConditionNode
{
    [NonSerialized] private int _count = 0;
    protected override void OnExit(Environment env)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStart(Environment env)
    {
        throw new System.NotImplementedException();
    }

    protected override State OnUpdate(Environment env)
    {
        State childState = NodeChildren[_count % NodeChildren.Count].update(env);
        if (childState == State.Success)
        {
            _count++;
            return State.Success;
        }
        else if (childState == State.Failure) 
        {
            return State.Failure;
        }
        return State.Running;
    }
}
