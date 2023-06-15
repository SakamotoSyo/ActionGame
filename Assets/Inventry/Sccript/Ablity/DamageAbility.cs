using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : IAbility
{
    [SerializeField] int _damage;
    public void Use(Evaluator evl)
    {
        CharacterBase enemy = default;
        float min = float.MaxValue;
        foreach(var e in evl.Enemy)
        {
            float distance = Vector3.Distance(evl.Player.transform.position, e.transform.position);
            if(min > distance)
            {
                min = distance;
                enemy = e;
            }
        }
        enemy.Damage(_damage);
    }
}
