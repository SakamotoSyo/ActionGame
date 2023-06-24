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
        _playerAnimation.Start();
    }

    private void OnEnable()
    {
        InputManager.Instance.SetEnterInput(InputManager.InputType.Attack, Attack);
        InputManager.Instance.SetEnterInput(InputManager.InputType.Menu, MenuOpen);
        InputManager.Instance.SetEnterInput(InputManager.InputType.Shield, ShieldBeginningControl);
        InputManager.Instance.SetExitInput(InputManager.InputType.Shield, ShieldEndControl);
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
        //ガードしていたら
        if (_playerState.HasFlag(PlayerState.Shield)) 
        {
            AddState(PlayerState.Damage);
            _playerMove.Knockback(enemyPos, _playerState);
            _playerAnimation.ShieldEffect();
            await _playerAnimation.ShieldDamageAnim();
            RemoveState(PlayerState.Damage);

            Invincible(_invincibleTime);
        }
        else if (!_playerState.HasFlag(PlayerState.Damage) && !_playerState.HasFlag(PlayerState.Invincible))
        {
        　　//ダメージを受ける
            if (_playerStatus.DownJudge(damage))
            {
                _playerStatus.ReceiveDamage(damage);

                AddState(PlayerState.Damage);
                _playerMove.Knockback(enemyPos, _playerState);
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

    /// <summary>
    /// ガードを始める一連の流れ
    /// </summary>
    public void ShieldBeginningControl()
    {
        AddState(PlayerState.Shield);
        _playerAnimation.ShieldAnim(true);
    }

    public void ShieldEndControl() 
    {
        RemoveState(PlayerState.Shield);
        _playerAnimation.ShieldAnim(false);
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
        if (!_playerState.HasFlag(PlayerState.Shield))
        {
            AddState(PlayerState.Attack);
            _playerAnimation.AttackAnim();
            await _playerAttack.Attack();
            RemoveState(PlayerState.Attack);
        }
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
    Shield = 32,
}