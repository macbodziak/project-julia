using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSingleTargetAction : BaseAction
{
    [SerializeField] AttackActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteLogic()
    {
        targets[0].ReceiveAttack(data.GetAttackInfo());
    }
}