using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachBuffAbility : IAbility
{
    [SerializeField] int _buff;
    public void Use(Evaluator evl)
    {
       // evl.Player.AttackBuff(_buff);
    }
}
