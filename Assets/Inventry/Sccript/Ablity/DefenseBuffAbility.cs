using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseBuffAbility : MonoBehaviour
{
    [SerializeField] int _buff;
    public void Use(Evaluator evl)
    {
        //evl.Player.DefenseBuff(_buff);
    }
}
