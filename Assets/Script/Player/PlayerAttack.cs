using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// Player�̍U���Ɋւ��邱�Ƃ��܂Ƃ߂郁�\�b�h
/// </summary>
[Serializable]
public class PlayerAttack : IMatchTarget
{
    [SerializeField] private Transform _myselfTransform;
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
    [Header("�߂��̓G��T�����߂�Collider")]
    [SerializeField] private Collider _serachCollider;
    private RaycastHit _hit;
    private Collider _targetCollider;

    public Vector3 TargetPosition
    {
        get
        {
            if (_targetCollider)
            {
                return _targetCollider.ClosestPoint(_myselfTransform.position);
            }
            else
            {
                return new Vector3(int.MaxValue, int.MaxValue);
            }
        }
    }

    public void Start()
    {
        foreach (var smb in _anim.GetBehaviours<MatchPositionSMB>())
        {
            smb.Target = this;
        }

        SearchClosestEnemy();
    }

    public void Update()
    {
    }

    public async UniTask Attack()
    {
        //�U����Animation���I�������ꍇ��Animarion���؂�ւ�����ꍇwhile�𔲂���
        while (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= _collisionDetectionStart &&
           _anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= _collisionDetectionEnd)
            {
                HitColliderChick();
            }
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// �����Collider�Ƀq�b�g���������ׂ�
    /// </summary>
    private void HitColliderChick()
    {
        BehaviorHelper.OnDrawSphere(_myselfTransform, _radius, Vector3.up, _maxDistance);
        var hitColliderArray = Physics.OverlapSphere(_myselfTransform.position + _myselfTransform.forward, _radius);
        if (hitColliderArray.Length == 0) return;
        Debug.Log("�U���������Ă��");
        for (int i = 0; i < hitColliderArray.Length; i++)
        {
            if (hitColliderArray[i].gameObject.TryGetComponent(out IDamageble damageCs))
            {
                damageCs.ReceiveDamage(_damage, _myselfTransform.position);
            }
        }
    }

    /// <summary>
    /// ��ԋ߂��G��T��
    /// </summary>
    private void SearchClosestEnemy()
    {
        _serachCollider.OnTriggerStayAsObservable()
        .Where(x => x.gameObject.GetComponent<BehaviourTreeRunner>() != null)
        .Subscribe(
         x =>
         {
             if (_targetCollider)
             {
                 if (Vector3.SqrMagnitude(_myselfTransform.position - _targetCollider.transform.position) >
                     Vector3.SqrMagnitude(_myselfTransform.position - x.transform.position))
                 {
                     _targetCollider = x;
                 }
             }
             else
             {
                 _targetCollider = x;
             }
         }
        );
    }
}