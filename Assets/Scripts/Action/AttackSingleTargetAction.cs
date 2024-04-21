using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackSingleTargetAction : BaseAction
{

    [SerializeField] float actionDuration = 1.2f;
    public override String Name()
    {
        return "Attack";
    }

    Unit targetUnit;

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        targetUnit = targets[0];
        animator.SetTrigger("Attack");
        ////Debug
        Debug.Log("StartAction with targets " + targets);

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    public override ActionType Type()
    {
        return ActionType.SingleEnemyTarget;
    }

    // public override void Update()
    // {
    //     if (isInProgress)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         if (elapsedTime >= actionDuration)
    //         {
    //             OnActionCompleted();
    //         }
    //     }
    // }

    public override bool ValidateArguments(List<Unit> targets)
    {
        if (targets.Count == 1)
        {
            return true;
        }

        return false;
    }

    protected override void ExecuteActionLogic()
    {
        Debug.Log("" + unit.gameObject + " attacking " + targetUnit.gameObject);
    }

    IEnumerator PerformAction()
    {
        yield return new WaitForSeconds(actionDuration);
        ExecuteActionLogic();
        OnActionCompleted();
        yield return null;
    }
}