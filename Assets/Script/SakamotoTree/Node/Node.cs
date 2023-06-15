using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

[Serializable]
public abstract class Node : ScriptableObject
{
    public enum State 
    {
        Running,
        Failure,
        Success
    }
   public Action<bool> CurrentStateView; 
   [NonSerialized] private State sendState = State.Failure;
   [NonSerialized] private State _state = State.Running;
   [NonSerialized] private bool _started = false;
    public string Guid;
    public Vector2 Position;
    [NonSerialized] protected CancellationTokenSource _token;

    public Node() 
    {
        _token = new CancellationTokenSource();
    }

    public State update(Environment env) 
    {
        if (!_started) 
        {
            OnStart(env);
            _started = true;
        }
        CurrentStateView?.Invoke(true);

        _state = OnUpdate(env);

        if (_state == State.Failure || _state == State.Success) 
        {
            OnExit(env);
            _started = false;
            CurrentStateView?.Invoke(false);
        }

        TreePlayerLoop(_token.Token);

        return _state;
    }

    private async void TreePlayerLoop(CancellationToken token) 
    {
        await UniTask.DelayFrame(2, cancellationToken: token);
        CurrentStateView?.Invoke(false);
    }

    protected abstract void OnStart(Environment env);
    protected abstract void OnExit(Environment env);
    protected abstract State OnUpdate(Environment env);

    public void Cancel()
    {
        _token.Cancel();
    }
}
