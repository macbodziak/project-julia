using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSingleTargetAction : BaseAction
{

    [SerializeField] private int damage;


    Unit targetUnit;

    public override void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        targetUnit = targets[0];
        animator.SetTrigger("Attack");

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    public override ActionType Type()
    {
        return ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteActionLogic()
    {
        targetUnit.TakeDamage(damage);
    }
}