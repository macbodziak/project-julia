using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction2 : BaseAction
{
    float elapsedTime;
    float actionTime = 1.0f;
    public override String Name()
    {
        return "Attack2";
    }

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Attack2");
        elapsedTime = 0f;
        ////Debug
        Debug.Log("StartAction with targets " + targets);

        OnActionStarted();
    }

    // public override void Update()
    // {
    //     if (isInProgress)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         if (elapsedTime >= actionTime)
    //         {
    //             OnActionCompleted();
    //         }
    //     }
    // }

    public override ActionType Type()
    {
        return ActionType.SingleEnemyTarget;
    }

    public override bool ValidateArguments(List<Unit> targets)
    {
        if (targets.Count == 2)
        {
            return true;
        }

        return false;
    }

    protected override void ExecuteActionLogic()
    {
        Debug.Log(gameObject + "  ExecuteActionLogic()");
    }
}
