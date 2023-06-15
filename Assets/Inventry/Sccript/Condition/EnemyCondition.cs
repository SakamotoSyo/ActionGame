using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCondition: ICondition
{
    public bool Check(Evaluator evl)
    {
        return evl.Enemy.Count > 0;
    }
}

