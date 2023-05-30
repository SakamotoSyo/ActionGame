using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class AttackNode : ActionNode
{
    [Header("çUåÇÇÃAnimParam")]
    [SerializeField] private string _attackParam;
    [NonSerialized] private string _previousAnimation;
    [NonSerialized] private bool _isAnimation;
    [NonSerialized] private bool _isComplete;
    protected override void OnExit(Environment env)
    {
        
    }

    protected override void OnStart(Environment env)
    {
       
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="env"></param>
    /// <returns></returns>
    protected override State OnUpdate(Environment env)
    {
        if (_isComplete && _isAnimation)
        {
            _isComplete = false;
            _isAnimation = false;
            return State.Success;
        }
        else if (!_isAnimation) 
        {
            _isAnimation = true;
            Attack(env);
        }

        return State.Running;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="env"></param>
    private async void Attack(Environment env)
    {
        env.MySelfAnim.SetTrigger(_attackParam);
        await UniTask.Delay(1000);
        await UniTask.WaitUntil(() => env.MySelfAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f);
        //Debug.Log("2");
        env.MySelfAnim.SetTrigger(_attackParam);
        await UniTask.WaitUntil(() => env.MySelfAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        _isComplete = true;
    }
}
