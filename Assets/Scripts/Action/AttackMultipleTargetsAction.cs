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
        foreach (Unit target in targets)
        {
            int damageDealt = UnityEngine.Random.Range(data.MinDamage, data.MaxDamage);
            target.TakeDamage(damageDealt);
        }
    }

}
