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
   public Action<bool> CurrentState; 
   [NonSerialized] private State sendState = State.Failure;
   [NonSerialized] private State _state = State.Running;
   [NonSerialized] private bool _started = false;
    public string Guid;
    public Vector2 Position;
    [NonSerialized] private CancellationTokenSource _token;

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
        CurrentState?.Invoke(true);

        _state = OnUpdate(env);

        if (_state == State.Failure || _state == State.Success) 
        {
            OnExit(env);
            _started = false;
            CurrentState?.Invoke(false);
        }

        TreePlayerLoop(_token.Token);

        return _state;
    }

    private async void TreePlayerLoop(CancellationToken token) 
    {
        await UniTask.DelayFrame(2, cancellationToken: token);
        CurrentState?.Invoke(false);
    }

    protected abstract void OnStart(Environment env);
    protected abstract void OnExit(Environment env);
    protected abstract State OnUpdate(Environment env);

    private void OnDestroy()
    {
        _token.Cancel();
    }
}
