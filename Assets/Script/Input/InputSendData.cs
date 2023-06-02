using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct InputSendData : IEquatable<InputSendData>
{
    public Vector3 LastMoveDir => _playerDir;

    [Tooltip("最後に動いた方向")]
    private Vector3 _playerDir;
    [Tooltip("動く方向")]
    private Vector3 _movement;
    [Tooltip("Inputをブロックするかどうか")]
    private bool _inputBlock;
    private bool _isJump;
    private bool _isAction;

    /// <summary>現在アクション中かどうか返す</summary>
    public bool Action
    {
        get {return _isAction && !_inputBlock; }
    }

    public bool Jump
    {
        get { return _isJump && !_inputBlock; }
    }
    /// <summary>現在の方向入力を返す</summary>
    public Vector3 MoveInput
    {
        get
        {
            if (_inputBlock)
            {
                return Vector3.zero;
            }
            return _movement;
        }
    }

    public void ReceiveData(Vector3 movement, Vector3 playerDir, bool action, bool Jump, bool inputBlock) 
    {
        _movement = movement;
        _playerDir = playerDir;
        _isAction = action;
        _isJump = Jump;
        _inputBlock = inputBlock;
    }

    /// <summary>Inputに関する入力を受け付けるかどうか変更する</summary>
    public void InputBlock()
    {
        _inputBlock = !_inputBlock;
    }

    public bool Equals(InputSendData other)
    {
        throw new NotImplementedException();
    }

}
