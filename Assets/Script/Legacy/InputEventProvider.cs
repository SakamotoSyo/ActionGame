using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using MessagePipe;

public class InputEventProvider : ITickable
{
    [Tooltip("��������")]
    private Vector3 _movement = new Vector3(0, 0, 0);
    [Tooltip("�Ō�ɓ���������")]
    private Vector3 _playerDir = new Vector3(0, 0, 0);
    [Tooltip("����Action����")]
    private bool _isAction = false;
    [Tooltip("���݃W�����v�����ǂ���")]
    private bool _isJump = false;
    private bool _isInputBlock = false;
    private readonly IPublisher<InputSendData> _inputPublisher;
    private InputSendData _inputSendData = new();

    public InputEventProvider(IPublisher<InputSendData> inputPublisher)
    {
        _inputPublisher = inputPublisher;
    }

    public void Tick()
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

        _inputSendData.ReceiveData(_movement, _playerDir, _isAction, _isJump, _isInputBlock);
        _inputPublisher.Publish(_inputSendData);
    }
}
