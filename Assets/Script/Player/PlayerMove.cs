using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _knockBackPower;
    [SerializeField] private GameObject _lookAtObj;
    [SerializeField] private Rigidbody _rb;

    [SerializeField]Transform _transform;
    Vector3 _dir = new Vector3(0, 0, 0);

    /// <summary>
    /// Playerを移動させる関数
    /// </summary>
    /// <param name="sendData">Userに入力されているInputのData</param>
    public void Move() 
    {
        _dir = new Vector3(InputManager.Instance.MoveDir.x, 0, InputManager.Instance.MoveDir.z);
        _dir = _mainCamera.transform.TransformDirection(_dir);

        _dir.y = 0;

        if (_dir != Vector3.zero)
        {
            Quaternion targetRotetion = Quaternion.LookRotation(_dir);
        }
        _rb.velocity = _dir.normalized * _moveSpeed + new Vector3(0, _rb.velocity.y, 0f);

        _transform.LookAt(new Vector3(_lookAtObj.transform.position.x, _transform.position.y, _lookAtObj.transform.position.z));
    }

    public void Knockback(Vector3 enemyPos, PlayerState playerState) 
    {
        if (playerState.HasFlag(PlayerState.Shield))
        {
            Vector3 distination = (_transform.position - enemyPos).normalized;
            _rb.AddForce(distination * _knockBackPower / 2, ForceMode.Impulse);
        }
        else 
        {
            Vector3 distination = (_transform.position - enemyPos).normalized;
            _rb.AddForce(distination * _knockBackPower, ForceMode.Impulse);
        }   
    }
}
