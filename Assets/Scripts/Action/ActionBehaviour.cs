using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

// Summary
// this is the base class for actions. To implement another action, the derived class
// needs to: 
//      implement the ExecuteLogic() method
//      override the Awake() method and set the right Type of Action

public class ActionBehaviour : MonoBehaviour
{

    [SerializeField] private ActionDefinition actionDefinition;
    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    private Action OnActionCompletedCallback;
    //member fields
    private Unit unit;
    private bool isInProgress;
    private Animator animator;

    private List<Unit> targets;

    public int ActionPointCost
    {
        get { return actionDefinition.ActionPointCost; }
    }

    public string Name
    {
        get { return actionDefinition.Name; }
    }

    public Sprite Icon
    {
        get { return actionDefinition.Icon; }
    }

    public TargetingModeType ActionType
    {
        get { return actionDefinition.TargetingMode; }
    }

    public int NumberOfTargets
    {
        get { return actionDefinition.NumberOfTargets; }
    }

    public bool IsInProgress { get => isInProgress; private set => isInProgress = value; }

    protected virtual void Awake()
    {
        IsInProgress = false;
        targets = new List<Unit>();
    }

    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }
    protected virtual void OnActionStarted()
    {
        IsInProgress = true;
    }

    // Summary
    // This method initiates the action, starts animation, receives all needed paramaters
    // the actual logic execution starts later and should be trigger bu the animation
    // via an animation event
    public void StartAction(List<Unit> targetList, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        targets.Clear();
        targets.AddRange(targetList);
        animator.SetTrigger(actionDefinition.AnimationTrigger);

        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    protected virtual void OnActionCompleted()
    {
        IsInProgress = false;
        OnActionCompletedCallback();
    }

    protected IEnumerator PerformAction()
    {
        unit.combatStats.CurrentActionPoints -= actionDefinition.ActionPointCost;
        yield return new WaitForSeconds(actionDefinition.Duration * 0.3f);
        actionDefinition.ExecuteLogic(unit, targets);
        //TO DO play audio
        yield return new WaitForSeconds(actionDefinition.Duration * 0.7f);
        OnActionCompleted();
        yield return null;
    }

    public float GetDuration()
    {
        return actionDefinition.Duration;
    }
}
