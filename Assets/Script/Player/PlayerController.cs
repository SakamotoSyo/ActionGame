using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using MessagePipe;
using VContainer;

public class PlayerController : MonoBehaviour
{
    [Inject] private ISubscriber<InputSendData> _inputSendSubscriber;
    [SerializeField] private PlayerMove _playerMove = new();
    [SerializeField] private PlayerAnimation _playerAnimation = new();
    private InputSendData _inputSendData;

    void Start()
    {
        _inputSendSubscriber.Subscribe(OnInputEventReceived).AddTo(this.GetCancellationTokenOnDestroy());
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        MoveControl();
    }

    /// <summary>
    /// ˆÚ“®‚Ìˆ—‚Ì—¬‚ê‚ğ§Œä‚·‚éŠÖ”
    /// </summary>
    private void MoveControl()
    {
        _playerMove.Move(_inputSendData);
        _playerAnimation.MoveAnimation(_inputSendData);
    }

    private void OnInputEventReceived(InputSendData sendData) 
    {
        _inputSendData = sendData;
    }

}
