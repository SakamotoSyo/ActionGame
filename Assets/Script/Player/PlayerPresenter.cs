using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerController _playerController;
    private PlayerStatus _playerStatus = new();

    void Start()
    {
        _playerController.SetPlayerStatus(_playerStatus);
        _playerStatus.PlayerMaxHp.Subscribe(_playerView.SetMaxHp).AddTo(this);
        _playerStatus.PlayerCurrentHp.Subscribe(_playerView.SetHpCurrent).AddTo(this);
    }

}
