using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Summary
// this is the base class for actions. To implement another action, the derived class
// needs to: 
//      implement the ExecuteLogic() method
//      override the Awake() method and set the right Type of Action

public abstract class BaseAction : MonoBehaviour
{

    protected BaseActionData baseData;
    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    protected Action OnActionCompletedCallback;
    //member fields
    protected Unit unit;
    protected bool isInProgress;
    protected Animator animator;

    protected List<Unit> targets;

    protected ActionType m_actionType;
    public int ActionPointCost
    {
        get { return baseData.ActionPointCost; }
    }

    public string Name
    {
        get { return baseData.Name; }
    }

    public Sprite Icon
    {
        get { return baseData.Icon; }
    }

    public ActionType actionType
    {
        get { return m_actionType; }
        protected set { m_actionType = value; }
    }

    protected virtual void Awake()
    {
        isInProgress = false;
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }

    protected virtual void OnActionStarted()
    {
        isInProgress = true;
    }

    // Summary
    // This method initiates the action, starts animation, receives all needed paramaters
    // the actual logic execution starts later and should be trigger bu the animation
    // via an animation event
    public void StartAction(List<Unit> targets, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        this.targets = targets;
        animator.SetTrigger(baseData.AnimationTrigger);

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
    }

    protected abstract void ExecuteLogic();

    protected IEnumerator PerformAction()
    {
        unit.ActionPoints -= baseData.ActionPointCost;
        yield return new WaitForSeconds(baseData.Duration * 0.3f);
        ExecuteLogic();
        //TO DO play audio
        yield return new WaitForSeconds(baseData.Duration * 0.7f);
        OnActionCompleted();
        yield return null;
    }

    public float GetDuration()
    {
        return baseData.Duration;
    }
}
