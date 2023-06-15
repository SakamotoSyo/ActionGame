using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ActorStatus
{
    public IReactiveProperty<float> MaxHp => _maxHp;
    public IReactiveProperty<float> CurrentHp => _currentHp;
    private ReactiveProperty<float> _maxHp = new();
    private ReactiveProperty<float> _currentHp = new();

    public ActorStatus() 
    {
        _maxHp.Value = 100;
        _currentHp.Value = 100;
    }

    public void ReceiveDamage(float damage) 
    {
        _currentHp.Value -= damage;
    }
}
