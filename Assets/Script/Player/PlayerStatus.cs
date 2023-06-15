using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerStatus
{
    public IObservable<float> PlayerCurrentHp => _playerCurrentHp;
    public IObservable<float> PlayerMaxHp => _playerMaxHp;

    private ReactiveProperty<float> _playerCurrentHp = new();
    private ReactiveProperty<float> _playerMaxHp = new();

    public PlayerStatus() 
    {
        _playerMaxHp.Value = 100;
        _playerCurrentHp.Value = 100;
    }

    public void ReceiveDamage(float damage) 
    {
        _playerCurrentHp.Value -= (int)damage;
    }

    public void RecoveryHp(float recovery) 
    {
        _playerCurrentHp.Value += recovery;
    }

    public bool DownJudge(float damage)
    {
        return 0 < _playerCurrentHp.Value - damage;
    }
}
