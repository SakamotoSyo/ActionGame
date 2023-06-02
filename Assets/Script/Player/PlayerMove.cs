using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _lookAtObj;
    [SerializeField] private Rigidbody _rb;

    [SerializeField]Transform _transform;
    Vector3 _dir = new Vector3(0, 0, 0);

    /// <summary>
    /// Player���ړ�������֐�
    /// </summary>
    /// <param name="sendData">User�ɓ��͂���Ă���Input��Data</param>
    public void Move(InputSendData sendData) 
    {
        _dir = new Vector3(sendData.MoveInput.x, 0, sendData.MoveInput.z);
        _dir = _mainCamera.transform.TransformDirection(_dir);

        _dir.y = 0;

        if (_dir != Vector3.zero)
        {
            Quaternion targetRotetion = Quaternion.LookRotation(_dir);
        }
        _rb.velocity = _dir.normalized * _moveSpeed + new Vector3(0, _rb.velocity.y, 0f);

        _transform.LookAt(new Vector3(_lookAtObj.transform.position.x, _transform.position.y, _lookAtObj.transform.position.z));
    }
}
