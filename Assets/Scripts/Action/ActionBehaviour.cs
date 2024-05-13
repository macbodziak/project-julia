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
    [SerializeField] private ActionDefinition _actionDefinition;
    [SerializeField]
    [ReadOnly] // TO DO - remove readonly attribute
    private int _cooldown;
    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    private Action OnActionCompletedCallback;
    //member fields
    private Unit _unit;
    private bool _isInProgress;
    private bool _available;
    private Animator _animator;
    private List<Unit> _targets;

    public int actionPointCost
    {
        get { return actionDefinition.ActionPointCost; }
    }

    public string Name
    {
        get { return actionDefinition.Name; }
    }

    public Sprite icon
    {
        get { return actionDefinition.Icon; }
    }

    public TargetingModeType targetingMode
    {
        get { return actionDefinition.TargetingMode; }
    }

    public int numberOfTargets
    {
        get { return actionDefinition.NumberOfTargets; }
    }

    public bool isInProgress { get => _isInProgress; private set => _isInProgress = value; }
    public ActionDefinition actionDefinition { get => _actionDefinition; private set => _actionDefinition = value; }
    public int cooldown { get => _cooldown; private set { SetCooldown(value); } }

    public bool available { get => _available; private set => _available = value; }

    private void SetCooldown(int arg)
    {
        _cooldown = arg;
        if (_cooldown > 0)
        {
            available = false;
        }
        else
        {
            _cooldown = 0;
            available = true;
        }
    }

    protected virtual void Awake()
    {
        isInProgress = false;
        available = true;
        _targets = new List<Unit>();
    }

    protected virtual void Start()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
    }
    protected virtual void OnActionStarted()
    {
        isInProgress = true;
    }

    public void DecrementCooldown()
    {
        cooldown--;
    }

    // Summary
    // This method initiates the action, starts animation, receives all needed paramaters
    // the actual logic execution starts later and should be trigger bu the animation
    // via an animation event
    public void StartAction(List<Unit> targetList, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        _targets.Clear();
        _targets.AddRange(targetList);
        _animator.SetTrigger(actionDefinition.AnimationTrigger);
        cooldown = actionDefinition.Cooldown;
        StartCoroutine(PerformAction());
        OnActionStarted();
    }

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
    }

    protected IEnumerator PerformAction()
    {
        _unit.combatStats.CurrentActionPoints -= actionDefinition.ActionPointCost;
        yield return new WaitForSeconds(actionDefinition.Duration * 0.3f);
        actionDefinition.ExecuteLogic(_unit, _targets);
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
