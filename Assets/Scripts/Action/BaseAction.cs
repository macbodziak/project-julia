using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class BaseAction : MonoBehaviour
{
    public enum ActionType
    {
        SingleEnemyTarget,
        MultipleEnemyTargets,
        AllEnemyTargets,
        SelfTarget,
        SingleAllyTarget,
        MultipleAllyTargets,
        AllAllyTargets,
        NoTarget
    };
    [SerializeField] private int actionPointCost;

    public int ActionPointCost
    {
        get { return actionPointCost; }
        private set { actionPointCost = value; }
    }

    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    protected Action OnActionCompletedCallback;
    //member fields
    protected Unit unit;
    protected bool isInProgress;
    protected Animator animator;
    [SerializeField] float actionDuration = 1.2f;

    [SerializeField] protected string actionName;

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

    public string Name()
    {
        return actionName;
    }


    public abstract ActionType Type();

    protected abstract void ExecuteActionLogic();

    public virtual int GetNumberOfTargets()
    {
        return 1;
    }
    protected virtual IEnumerator PerformAction()
    {
        unit.ActionPoints -= actionPointCost;
        yield return new WaitForSeconds(actionDuration * 0.3f);
        ExecuteActionLogic();
        //TO DO play audio
        yield return new WaitForSeconds(actionDuration * 0.7f);
        OnActionCompleted();
        yield return null;
    }

}
