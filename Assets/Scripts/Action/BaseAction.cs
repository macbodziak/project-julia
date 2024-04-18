using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAction : MonoBehaviour
{
    //Events:
    public event EventHandler ActionStarted;
    public event EventHandler ActionCompleted;

    //member fields
    protected Unit unit;
    bool isInProgress;

    protected virtual void Awake()
    {
        isInProgress = false;
        unit = GetComponent<Unit>();
        Debug.Log("Base Action  protected virtual private void Awake" + unit.name);
    }

    protected virtual void OnActionStarted()
    {
        isInProgress = true;
        ActionStarted?.Invoke(this, EventArgs.Empty);
    }
    public abstract void StartAction();

    protected virtual void OnActionCompleted()
    {
        isInProgress = false;
        ActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public abstract string Name();
}
