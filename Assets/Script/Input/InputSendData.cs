using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct InputSendData : IEquatable<InputSendData>
{
    public Vector3 LastMoveDir => _playerDir;

    [Tooltip("�Ō�ɓ���������")]
    private Vector3 _playerDir;
    [Tooltip("��������")]
    private Vector3 _movement;
    [Tooltip("Input���u���b�N���邩�ǂ���")]
    private bool _inputBlock;
    private bool _isJump;
    private bool _isAction;

    /// <summary>���݃A�N�V���������ǂ����Ԃ�</summary>
    public bool Action
    {
        get {return _isAction && !_inputBlock; }
    }

    public bool Jump
    {
        get { return _isJump && !_inputBlock; }
    }
    /// <summary>���݂̕������͂�Ԃ�</summary>
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

    /// <summary>Input�Ɋւ�����͂��󂯕t���邩�ǂ����ύX����</summary>
    public void InputBlock()
    {
        _inputBlock = !_inputBlock;
    }

    public bool Equals(InputSendData other)
    {
        throw new NotImplementedException();
    }

}
