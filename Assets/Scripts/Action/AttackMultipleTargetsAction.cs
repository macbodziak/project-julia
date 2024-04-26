using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMultipleTargetsAction : BaseAction, IMultipleTargets
{
    [SerializeField] private int damage;

    [SerializeField] private int targetNumber;

    public int GetNumberOfTargets()
    {
        return targetNumber;
    }

    protected override void Awake()
    {
        base.Awake();
        actionType = ActionType.MultipleEnemyTargets;
    }

    protected override void ExecuteActionLogic()
    {
        foreach (Unit target in targets)
        {
            target.TakeDamage(damage);
        }
    }

}
