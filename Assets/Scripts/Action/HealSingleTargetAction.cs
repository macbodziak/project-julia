using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSingleTargetAction : BaseAction
{
    [SerializeField] HealActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.SingleAllyTarget;
    }

    protected override void ExecuteLogic()
    {
        targets[0].ReceiveHealing(data.GetHealingInfo());
    }
}