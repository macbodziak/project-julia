using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    [SerializeField] BaseActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.NoTarget;
    }

    protected override void ExecuteLogic()
    {
        // does nothing usefull other than playing an animation
    }
}