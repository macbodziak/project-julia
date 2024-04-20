using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackAction : BaseAction
{
    float elapsedTime;
    const float actionTime = 3.0f;
    public override String Name()
    {
        return "Attack";
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

    public override ActionType Type()
    {
        return ActionType.SingleEnemyTarget;
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

    public override bool ValidateArguments(List<Unit> targets)
    {
        if (targets.Count == 1)
        {
            return true;
        }

        return false;
    }
}