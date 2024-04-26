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

    protected List<Unit> targets;

    protected ActionType m_actionType;
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
        animator.SetTrigger(data.AnimationTrigger);

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
    }

    protected abstract void ExecuteActionLogic();

    protected IEnumerator PerformAction()
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
