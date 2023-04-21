using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    public Node Child;
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
        return Child.update(env);
    }
}
