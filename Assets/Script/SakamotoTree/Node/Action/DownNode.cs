using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownNode : ActionNode
{
    protected override void OnExit(Environment env)
    {
        
    }

    protected override void OnStart(Environment env)
    {
        env.MySelfAnim.Play("Down");
        env.MySelfRb.velocity = Vector3.zero;
        Debug.Log("Ž€‚ñ‚¾");
    }

    protected override State OnUpdate(Environment env)
    {
        if (env.MySelfAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99) 
        {
            Destroy(env.MySelf);
        }
        return State.Running;
    }
}
