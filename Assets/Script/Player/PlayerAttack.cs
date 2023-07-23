using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using UniRx.Triggers;
using UniRx;

/// <summary>
/// Playerの攻撃に関することをまとめるメソッド
/// </summary>
[Serializable]
public class PlayerAttack : IMatchTarget
{
    [SerializeField] private Transform _myselfTransform;
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
    [Header("近くの敵を探すためのCollider")]
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
        //攻撃のAnimationが終了した場合とAnimarionが切り替わった場合whileを抜ける
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
    /// 特定のColliderにヒットしたか調べる
    /// </summary>
    private void HitColliderChick()
    {
        BehaviorHelper.OnDrawSphere(_myselfTransform, _radius, Vector3.up, _maxDistance);
        var hitColliderArray = Physics.OverlapSphere(_myselfTransform.position + _myselfTransform.forward, _radius);
        if (hitColliderArray.Length == 0) return;
        Debug.Log("攻撃当たってるよ");
        for (int i = 0; i < hitColliderArray.Length; i++)
        {
            if (hitColliderArray[i].gameObject.TryGetComponent(out IDamageble damageCs))
            {
                damageCs.ReceiveDamage(_damage, _myselfTransform.position);
            }
        }
    }

    /// <summary>
    /// 一番近い敵を探す
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