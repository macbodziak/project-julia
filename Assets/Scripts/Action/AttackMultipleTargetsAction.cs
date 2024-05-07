using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMultipleTargetsAction : BaseAction, IMultipleTargets
{
    [SerializeField] MultipleAttackActionData data;
    public int GetNumberOfTargets()
    {
        return data.NumberOfTargets;
    }

    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.MultipleEnemyTargets;
    }

    protected override void ExecuteLogic()
    {
        CombatStats combatStats = GetComponent<CombatStats>();
        AttackInfo attack = data.GetAttackInfo(combatStats);
        foreach (Unit target in targets)
        {
            target.combatStats.ReceiveAttack(attack);
        }
    }

}
