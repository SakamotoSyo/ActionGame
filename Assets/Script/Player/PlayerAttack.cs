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
    [Header("�A�j���[�V�����̂ǂ̃^�C�~���O����U��������o����")]
    [SerializeField] private float _collisionDetectionStart, _collisionDetectionEnd;
    [Header("�����蔻��̑傫��")]
    [SerializeField] private float _radius;
    [Header("�����蔻��̒���")]
    [SerializeField] private float _maxDistance;
    [Header("�����蔻��̈ʒu����")]
    [SerializeField] private Vector3 _offset;
    [Header("����ɗ^����_���[�W")]
    [SerializeField] private float _damage;
    private RaycastHit _hit;

    public void Update() 
    {
    }

    public async UniTask Attack() 
    {
        var AttackName = _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //�U����Animation���I�������ꍇ��Animarion���؂�ւ�����ꍇwhile�𔲂���
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
                        Debug.Log("10�̃_���[�W��^����");
                        damageCs.ReceiveDamage(_damage, _transform.position);
                    }
                }
            }

            await UniTask.Yield();
        }
    }
}
