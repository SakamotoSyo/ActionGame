using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimation
{
    [SerializeField] private Animator _animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MoveAnimation() 
    {
        _animator.SetBool("Move", true);
    }
}
