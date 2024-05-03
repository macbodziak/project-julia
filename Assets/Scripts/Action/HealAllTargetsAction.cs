using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class HealAllTargetsAction : BaseAction
{
    [SerializeField] HealActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.AllAllyTargets;
    }

    protected override void ExecuteLogic()
    {
        foreach (Unit target in targets)
        {
            target.ReceiveHealing(data.GetHealingInfo());
        }
    }

}
