using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageble
{
    public PlayerState PlayerState => _playerState;
    [SerializeField] private PlayerMove _playerMove = new();
    [SerializeField] private PlayerAnimation _playerAnimation = new();
    [Tooltip("デフォルトの無敵時間")]
    [SerializeField] private float _invincibleTime = 0.5f;
    [SerializeField] private PlayerAttack _playerAttack = new();
    [SerializeField] private GameObject _menuObj;
    private PlayerStatus _playerStatus;
    private PlayerState _playerState;

    private void Awake()
    {
       
    }

    void Start()
    {
        InputManager.Instance.SetEnterInput(InputManager.InputType.Attack, Attack);
        InputManager.Instance.SetEnterInput(InputManager.InputType.Menu, MenuOpen);
        _playerAnimation.Start();
    }

    private void Update()
    {
        _playerAnimation.Update();
        _playerAttack.Update();
       _playerAnimation.MoveAnimation();
    }

    private void FixedUpdate()
    {
        MoveControl();
    }

    /// <summary>
    /// 移動の処理の流れを制御する関数
    /// </summary>
    private void MoveControl()
    {
        if (!_playerState.HasFlag(PlayerState.Damage) && !_playerState.HasFlag(PlayerState.Attack)) 
        {
            _playerMove.Move();
        }
    }

    /// <summary>
    /// ダメージの処理の流れを制御する
    /// </summary>
    /// <param name="damage"></param>
    public async void ReceiveDamage(float damage, Vector3 enemyPos)
    {
        if (!_playerState.HasFlag(PlayerState.Damage))
        {
            if (_playerStatus.DownJudge(damage))
            {
                _playerStatus.ReceiveDamage(damage);

                AddState(PlayerState.Damage);
                _playerMove.Knockback(enemyPos);
                await _playerAnimation.AddDamageAnim();
                RemoveState(PlayerState.Damage);

                Invincible(_invincibleTime);
            }
            else 
            {
                //死んだときの処理
            }
            
        }
    }

    public void Recovery(int recovery) 
    {
      _playerStatus.RecoveryHp(recovery);
    }

    /// <summary>
    /// 引数で指定された時間無敵になる
    /// </summary>
    /// <param name="invincibleTime"></param>
    public async void Invincible(float invincibleTime) 
    {
        AddState(PlayerState.Invincible);
        await UniTask.Delay(TimeSpan.FromSeconds(invincibleTime));
        RemoveState(PlayerState.Invincible);
    }

    private async void Attack() 
    {
        AddState(PlayerState.Attack);
        _playerAnimation.AttackAnim();
        await _playerAttack.Attack();
        RemoveState(PlayerState.Attack);
    }

    private void MenuOpen() 
    {
        if (_playerState.HasFlag(PlayerState.MenuOpen)) 
        {
            RemoveState(PlayerState.MenuOpen);
            _menuObj.SetActive(false);
        }
        else 
        {
            AddState(PlayerState.MenuOpen);
            _menuObj.SetActive(true);
        }
    }

    public void SetPlayerStatus(PlayerStatus playerStatus) 
    {
        _playerStatus = playerStatus;
    }

    public void AddState(PlayerState state)
    {
        _playerState |= state;
    }
    public void RemoveState(PlayerState state)
    {
        _playerState &= ~state;
    }
}

[Serializable]
[Flags]
public enum PlayerState
{
    Run = 1,
    Attack = 2,
    Damage = 4,
    Invincible = 8,
    MenuOpen = 16,
}