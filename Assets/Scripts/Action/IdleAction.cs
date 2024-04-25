using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : BaseAction
{
    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        animator.SetTrigger("Confused");

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    public override ActionType Type()
    {
        return ActionType.NoTarget;
    }

    protected override void ExecuteActionLogic()
    {
        Debug.Log(gameObject + " is doing nothing...");
    }
}