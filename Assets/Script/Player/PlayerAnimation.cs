using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class PlayerAnimation
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _shieldEffectPos;
    [SerializeField] private GameObject _shieldEffectObj;

    public void Start() 
    {
       
    }

    public void Update() 
    {

    }

    public void MoveAnimation() 
    {
        if (InputManager.Instance.MoveDir.x != 0 || InputManager.Instance.MoveDir.z != 0)
        {
            _animator.SetBool("Move", true);
        }
        else 
        {
            _animator.SetBool("Move", false);
        }
    }

    public void AttackAnim() 
    {
        _animator.SetTrigger("Attack");
    }

    public async UniTask AddDamageAnim() 
    {
        _animator.Play("Damage");
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
    }

    public async UniTask ShieldDamageAnim() 
    {
        _animator.SetTrigger("ShieldDamage");
        await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
    }

    public void ShieldEffect() 
    {
        Object.Instantiate(_shieldEffectObj, _shieldEffectPos.position, _shieldEffectPos.rotation);
    }

    public void ShieldAnim(bool playBool) 
    {
        _animator.SetBool("Shield", playBool);
    }
}
