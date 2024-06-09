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

[System.Serializable]
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
    [SerializeField][ReadOnly] private bool _isInProgress;
    private bool _ready;
    private Animator _animator;
    private List<Unit> _targets;
    private ActionController _actionController;


    public int ActionPointCost
    {
        get { return actionDefinition.ActionPointCost; }
    }

    public int PowerPointCost
    {
        get { return actionDefinition.PowerPointCost; }
    }

    public string Name
    {
        get { return actionDefinition.Name; }
    }

    public Sprite Icon
    {
        get { return actionDefinition.Icon; }
    }

    public TargetingMode targetingMode
    {
        get { return actionDefinition.TargetingMode; }
    }

    public int NumberOfTargets
    {
        get { return actionDefinition.NumberOfTargets; }
    }

    public bool isInProgress { get => _isInProgress; private set => _isInProgress = value; }
    public ActionDefinition actionDefinition { get => _actionDefinition; private set => _actionDefinition = value; }
    public int Cooldown { get => _cooldown; private set { SetCooldown(value); } }

    public bool Ready { get => _ready; private set => _ready = value; }


    private void SetCooldown(int arg)
    {
        _cooldown = arg;
        if (_cooldown > 0)
        {
            Ready = false;
        }
        else
        {
            _cooldown = 0;
            Ready = true;
        }
    }


    protected virtual void Awake()
    {
        isInProgress = false;
        Ready = true;
        _targets = new List<Unit>();
    }


    protected virtual void Start()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
        _actionController = GetComponent<ActionController>();
    }


    protected virtual void OnActionStarted()
    {
        isInProgress = true;
    }


    public void DecrementCooldown()
    {
        Cooldown--;
    }


    // Summary
    // This method initiates the action, starts animation, receives all needed paramaters
    // the actual logic execution starts later and should be trigger by the animation
    // via an animation event
    public void StartAction(List<Unit> targetList, Action onActionComplete)
    {
        this.OnActionCompletedCallback = onActionComplete;
        _targets.Clear();
        _targets.AddRange(targetList);

        _unit.combatStats.CurrentActionPoints -= actionDefinition.ActionPointCost;
        _unit.combatStats.CurrentPowerPoints -= actionDefinition.PowerPointCost;
        Cooldown = actionDefinition.Cooldown;

        _animator.SetTrigger(actionDefinition.AnimationTrigger);

        _actionController.RegisterActiveAction(this);

        GetComponent<SoundController>().PlayClip(_actionDefinition.SoundEffect);
        OnActionStarted();
    }

    // Summary
    // This method is invoked by the ActionController after recieving an animation event 
    // signaling that the animation has stopped playing
    public virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
    }


    public void ExecuteLogic()
    {
        _actionDefinition.ExecuteLogic(_unit, _targets);
    }

}
