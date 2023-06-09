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
    [NonSerialized] private State sendState = State.Failure;
    [NonSerialized] public State CurrentState = State.Running;
    [NonSerialized] public bool Started = false;
    public string Guid;
    [Header("Nodeの名前")]
    public string TitleName;
    public Vector2 Position;
    public Vector3 NodeScale;
    [NonSerialized] protected CancellationTokenSource _token;
    [Header("説明")]
    [TextArea] public string Description;

    public Node()
    {
        TitleName = name;
        _token = new CancellationTokenSource();
    }

    public State update(Environment env)
    {
        if (!Started)
        {
            OnStart(env);
            Started = true;
        }

        CurrentState = OnUpdate(env);

        if (CurrentState == State.Failure || CurrentState == State.Success)
        {
            OnExit(env);
            Started = false;
        }

        return CurrentState;
    }

    protected abstract void OnStart(Environment env);
    protected abstract void OnExit(Environment env);
    protected abstract State OnUpdate(Environment env);

    public void Cancel()
    {
        _token.Cancel();
    }

    public Node Clone()
    {
        Node node = Instantiate(this);
        return node;
    }
}
