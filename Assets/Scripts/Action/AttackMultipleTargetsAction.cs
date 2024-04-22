using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMultipleTargetsAction : BaseAction
{


    [SerializeField] float actionDuration = 1.2f;
    [SerializeField] private int damage;
    [SerializeField] private int numberOfTargets;

    [SerializeField] private bool attacksAll;
    private
    // [SerializeField] private string actionName;


    List<Unit> targetList;

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        targetList = targets;
        animator.SetTrigger("Attack2");

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    public override ActionType Type()
    {
        if (attacksAll)
        {
            return BaseAction.ActionType.AllEnemyTargets;
        }
        return BaseAction.ActionType.MultipleEnemyTargets;
    }

    public override bool ValidateArguments(List<Unit> targets)
    {
        if (targets.Count >= 1)
        {
            return true;
        }

        return false;
    }

    protected override void ExecuteActionLogic()
    {

        foreach (Unit target in targetList)
        {
            Debug.Log("" + unit.gameObject + " attacking " + target.gameObject);
            target.TakeDamage(damage);
        }
    }

    IEnumerator PerformAction()
    {
        yield return new WaitForSeconds(actionDuration);
        ExecuteActionLogic();
        OnActionCompleted();
        yield return null;
    }

    public override int GetNumberOfTargets()
    {
        return numberOfTargets;
    }
}
