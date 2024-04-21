using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class BaseAction : MonoBehaviour
{
    public enum ActionType { SingleEnemyTarget };

    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    protected Action OnActionCompletedCallback;
    //member fields
    protected Unit unit;
    protected bool isInProgress;
    protected Animator animator;


    protected virtual void Awake()
    {
        isInProgress = false;
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }

    // public abstract void Update();

    protected virtual void OnActionStarted()
    {
        isInProgress = true;
    }

    // Summary
    // This method initiates the action, starts animation, receives all needed paramaters
    // the actual logic execution starts later and should be trigger bu the animation
    // via an animation event
    public abstract void StartAction(List<Unit> targets, Action onActionComplete);

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
    }

    public abstract string Name();

    public abstract ActionType Type();

    public abstract bool ValidateArguments(List<Unit> targets);

    protected abstract void ExecuteActionLogic();

    private void OnExecuteActionAnimationEventTriggered()
    {
        ExecuteActionLogic();
    }

}
