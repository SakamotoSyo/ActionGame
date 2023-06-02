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

    public void MoveAnimation(InputSendData input) 
    {
        if (input.MoveInput.x != 0 || input.MoveInput.z != 0)
        {
            _animator.SetBool("Move", true);
        }
        else 
        {
            _animator.SetBool("Move", false);
        }
        
    }
}
