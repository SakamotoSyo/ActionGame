using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInput
{
    public Vector3 PlayerDir => _playerDir;
    public Vector3 LastMoveDir => _playerDir;
    //public int PlayerNum => _playerNum;

    //[Header("自分自身のプレイヤー番号")]
    //[SerializeField] int _playerNum;

    [Tooltip("動く方向")]
    Vector3 _movement = new Vector3(0, 0, 0);
    [Tooltip("最後に動いた方向")]
    Vector3 _playerDir = new Vector3(0, 0, 0);
    [Tooltip("現在Action中か")]
    bool _isAction = false;
    [Tooltip("現在ジャンプ中かどうか")]
    bool _isJump = false;
    [Tooltip("Inputをブロックするかどうか")]
    bool _inputBlock = true;

    /// <summary>現在アクション中かどうか返す</summary>
    public bool Action
    {
        get { return _isAction && !_inputBlock; }
    }

    public bool Jump
    {
        get { return _isJump && !_inputBlock; }
    }
    /// <summary>現在の方向入力を返す</summary>
    public Vector2 MoveInput
    {
        get
        {
            if (_inputBlock)
            {
                return Vector2.zero;
            }
            return _movement;
        }
    }

    public void Update()
    {
        _movement.x = Input.GetAxisRaw($"Horizontal");
        _movement.z = Input.GetAxisRaw($"Vertical");

        if (Vector3.zero != _movement)
        {
            _playerDir = _movement;
        }

        if (Input.GetButtonDown($"Action"))
        {
            _isAction = true;
        }
        else
        {
            _isAction = false;
        }

        if (Input.GetButtonDown($"Jump"))
        {
            _isJump = true;
        }
        else
        {
            _isJump = false;
        }
    }


    /// <summary>Inputに関する入力を受け付けるかどうか変更する</summary>
    public void InputBlock()
    {
        _inputBlock = !_inputBlock;
    }
}
