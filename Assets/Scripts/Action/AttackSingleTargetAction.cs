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
        CombatStats combatStats = GetComponent<CombatStats>();
        AttackInfo attack = data.GetAttackInfo(combatStats);
        targets[0].combatStats.ReceiveAttack(attack);
    }
}