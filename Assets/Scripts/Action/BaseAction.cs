using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class BaseAction : MonoBehaviour
{

    [SerializeField] protected ActionData data;
    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    protected Action OnActionCompletedCallback;
    //member fields
    protected Unit unit;
    protected bool isInProgress;
    protected Animator animator;

    public int ActionPointCost
    {
        get { return data.ActionPointCost; }
    }

    public string Name
    {
        get { return data.Name; }
    }

    public Sprite Icon
    {
        get { return data.Icon; }
    }
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




    public abstract ActionType Type();

    protected abstract void ExecuteActionLogic();

    public virtual int GetNumberOfTargets()
    {
        return 1;
    }
    protected virtual IEnumerator PerformAction()
    {
        unit.ActionPoints -= data.ActionPointCost;
        yield return new WaitForSeconds(data.Duration * 0.3f);
        ExecuteActionLogic();
        //TO DO play audio
        yield return new WaitForSeconds(data.Duration * 0.7f);
        OnActionCompleted();
        yield return null;
    }

}
