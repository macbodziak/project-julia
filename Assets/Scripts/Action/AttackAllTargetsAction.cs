using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class AttackAllTargetsAction : BaseAction
{
    [SerializeField] AttackActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.AllEnemyTargets;
    }

    protected override void ExecuteLogic()
    {
        foreach (Unit target in targets)
        {
            target.combatStats.ReceiveAttack(data.GetAttackInfo());
        }
    }

}
