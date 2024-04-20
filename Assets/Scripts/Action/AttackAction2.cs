using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction2 : BaseAction
{
    float elapsedTime;
    const float actionTime = 1.0f;
    public override String Name()
    {
        return "Attack2";
    }

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Attack");
        ////Debug
        Debug.Log("StartAction with targets " + targets);
        elapsedTime = 0f;
        OnActionStarted();
    }

    public override void Update()
    {
        if (isInProgress)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= actionTime)
            {
                OnActionCompleted();
            }
        }
    }

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
}
