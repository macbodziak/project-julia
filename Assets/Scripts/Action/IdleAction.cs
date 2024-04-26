using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    protected override void Awake()
    {
        base.Awake();
        actionType = ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteActionLogic()
    {
        Debug.Log(gameObject + " is doing nothing...");
    }
}