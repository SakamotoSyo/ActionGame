using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State 
    {
        Running,
        Failure,
        Success
    }

    private State _state = State.Running;
    private bool _started = false;
    public string Guid;
    public Vector2 Position;

    public State update(Environment env) 
    {
        if (!_started) 
        {
            OnStart(env);
            _started = true;
        }

        _state = OnUpdate(env);

        if (_state == State.Failure || _state == State.Success) 
        {
            OnExit(env);
            _started = false;
        }

        return _state;
    }

    protected abstract void OnStart(Environment env);
    protected abstract void OnExit(Environment env);
    protected abstract State OnUpdate(Environment env);
}
