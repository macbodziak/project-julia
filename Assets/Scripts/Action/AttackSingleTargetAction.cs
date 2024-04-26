using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSingleTargetAction : BaseAction
{

    [SerializeField] private int damage;
    protected override void Awake()
    {
        base.Awake();
        actionType = ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteActionLogic()
    {
        targets[0].TakeDamage(damage);
    }
}