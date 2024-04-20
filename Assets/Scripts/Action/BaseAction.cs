using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class BaseAction : MonoBehaviour
{
    public enum ActionType { SingleEnemyTarget };
    //Events:
    // public event EventHandler ActionStarted;
    // public event EventHandler ActionCompleted;
    // this delegate will be used to pass a private function form the Action MAnager to know when action has completed
    protected Action OnActionCompletedCallback;
    //member fields
    protected Unit unit;
    protected bool isInProgress;


    protected virtual void Awake()
    {
        isInProgress = false;
        unit = GetComponent<Unit>();
    }

    public abstract void Update();

    protected virtual void OnActionStarted()
    {
        isInProgress = true;
        // InputManager.Instance.SetInputState(InputManager.State.InputBlocked);
        // ActionStarted?.Invoke(this, EventArgs.Empty);
    }
    public abstract void StartAction(List<Unit> targets, Action onActionComplete);

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        OnActionCompletedCallback();
        // ActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public abstract string Name();

    public abstract ActionType Type();

    public abstract bool ValidateArguments(List<Unit> targets);
}
