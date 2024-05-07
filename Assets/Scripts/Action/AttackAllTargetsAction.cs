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
        CombatStats combatStats = GetComponent<CombatStats>();
        AttackInfo attack = data.GetAttackInfo(combatStats);
        foreach (Unit target in targets)
        {
            target.combatStats.ReceiveAttack(attack);
        }
    }

}
