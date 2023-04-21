using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [SerializeField] GameObject _mainCamera;
    [SerializeField] float _moveSpeed;
    [SerializeField] GameObject _lookAtObj;

    Transform _transform;
    Vector3 _dir = new Vector3(0, 0, 0);
    Rigidbody _rb;

    public void Init()
    {
        
    }
    
    public void Update()
    {
        
    }

    public void FixedUpdate() 
    {

    }

    private void Move() 
    {
        _dir = new Vector3(_inputManagerIns.MoveDir.x, 0, _inputManagerIns.MoveDir.y);
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
