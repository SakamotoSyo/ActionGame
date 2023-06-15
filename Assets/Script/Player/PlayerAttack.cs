using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

[Serializable]
public class PlayerAttack
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _anim;
    [Header("アニメーションのどのタイミングから攻撃判定を出すか")]
    [SerializeField] private float _collisionDetectionStart, _collisionDetectionEnd;
    [Header("当たり判定の大きさ")]
    [SerializeField] private float _radius;
    [Header("当たり判定の長さ")]
    [SerializeField] private float _maxDistance;
    [Header("当たり判定の位置調整")]
    [SerializeField] private Vector3 _offset;
    [Header("相手に与えるダメージ")]
    [SerializeField] private float _damage;
    private RaycastHit _hit;

    public void Update() 
    {
    }

    public async UniTask Attack() 
    {
        var AttackName = _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //攻撃のAnimationが終了した場合とAnimarionが切り替わった場合whileを抜ける
        while (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.99 
            && AttackName == _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name) 
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= _collisionDetectionStart &&
           _anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= _collisionDetectionEnd)
            {
                BehaviorHelper.OnDrawSphere(_transform, _radius, Vector3.up, _maxDistance);
                var isHit = Physics.SphereCast(_transform.position + _offset, _radius,
                    _transform.forward, out _hit, _maxDistance);
                if (isHit)
                {
                    if (_hit.collider.gameObject.TryGetComponent(out IDamageble damageCs))
                    {
                        Debug.Log("10のダメージを与えた");
                        damageCs.ReceiveDamage(_damage, _transform.position);
                    }
                }
            }

            await UniTask.Yield();
        }
    }
}
