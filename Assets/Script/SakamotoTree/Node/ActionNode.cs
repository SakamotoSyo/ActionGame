using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
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
        throw new System.NotImplementedException();
    }
}
