using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMultipleTargetsAction : BaseAction
{
    [SerializeField] private int damage;
    [SerializeField] private int numberOfTargets;

    [SerializeField] private bool attacksAll;


    private List<Unit> targetList;

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        targetList = targets;
        animator.SetTrigger("AttackCombo");

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    public override ActionType Type()
    {
        if (attacksAll)
        {
            return ActionType.AllEnemyTargets;
        }
        return ActionType.MultipleEnemyTargets;
    }

    protected override void ExecuteActionLogic()
    {

        foreach (Unit target in targetList)
        {
            target.TakeDamage(damage);
        }
    }

    public override int GetNumberOfTargets()
    {
        return numberOfTargets;
    }
}
