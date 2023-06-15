using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] int _hp;
    [SerializeField] int _attack;
    [SerializeField] int _defense;

    public int HP { get => _hp; set => _hp = value; }
    public int Attack { get => _attack; set => _attack = value; }
    public int Defense { get => _defense; set => _defense = value; }

    public void Damage(int damage)
    {
        _hp -= damage;
    }

    public void Recovery(int recovery)
    {
        _hp += recovery;
    }

    public void AttackBuff(int attackBuff)
    {
        _attack += attackBuff;
    }

    public void AttackDeBuff(int attackDeBuff)
    {
        _attack += attackDeBuff;
    }

    public void DefenseBuff(int defenseBuff)
    {
        _attack += defenseBuff;
    }

    public void DefenseDeBuff(int defenseDeBuff)
    {
        _attack += defenseDeBuff;
    }
}