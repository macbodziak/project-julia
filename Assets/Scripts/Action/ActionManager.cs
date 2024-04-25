using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionManager : MonoBehaviour
{
    public event EventHandler SelectedUnitChangedEvent;
    public event EventHandler SelectedActionChangedEvent;
    public event EventHandler SelectedTargetsChangedEvent;
    public event EventHandler ActionCompletedEvent;
    private static ActionManager _instance;
    //Debug - Serialized for debugging only
    [SerializeField] private Unit selectedUnit;
    //Debug - Serialized for debugging only
    [SerializeField] private List<Unit> TargetList;
    //Debug - Serialized for debugging only
    [SerializeField] private BaseAction selectedAction;

    public Unit SelectedUnit
    {
        get { return selectedUnit; }
        set { SetSelectedUnit(value); }
    }

    public BaseAction SelectedAction
    {
        get { return selectedAction; }
        set { SetSelectedAction(value); }
    }

    public static ActionManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            InitializeOnAwake();
        }
    }

    private void SetSelectedUnit(Unit newSelectedUnit)
    {
        //Unit Unselected
        if (newSelectedUnit == null)
        {
            if (selectedUnit != null)
            {
                selectedUnit.SetSelectionVisual(false);
                selectedUnit = null;
                SelectedUnitChangedEvent?.Invoke(this, EventArgs.Empty);
                //when unit sellection gets canceled we also need to cancel action selection
                SetSelectedAction(null);
            }
            return;
        }

        //Clicked already selected unit
        if (newSelectedUnit == selectedUnit)
        {
            return;
        }

        //Change unit selection
        if (selectedUnit != null)
        {
            selectedUnit.SetSelectionVisual(false);
        }
        selectedUnit = newSelectedUnit;
        selectedUnit.SetSelectionVisual(true);
        PrintUnitActions();
        SelectedUnitChangedEvent?.Invoke(this, EventArgs.Empty);

    }

    public void ClearUnitSelection()
    {
        selectedUnit = null;
    }

    public void PrintUnitActions()
    {
        if (selectedUnit == null)
        {
            return;
        }

        List<BaseAction> actions = selectedUnit.GetActionList();

        String debugString = "Actions available: ";
        int i = 1;
        foreach (BaseAction action in actions)
        {
            debugString += i + " " + action.Name() + " | ";
            i++;
        }
        Debug.Log(debugString);
    }

    private void SetSelectedAction(BaseAction newSelectedAction)
    {
        //Action Unselected
        if (newSelectedAction == null)
        {
            if (selectedAction != null)
            {
                selectedAction = null;
                SelectedActionChangedEvent?.Invoke(this, EventArgs.Empty);
            }
            return;
        }

        //Clicked already selected Action
        if (newSelectedAction == selectedAction)
        {
            return;
        }

        //Change Action selection
        if (newSelectedAction.ActionPointCost <= selectedUnit.ActionPoints)
        {

            selectedAction = newSelectedAction;

            InputManager.State inputState = GetInputStateBasedOnActionType(selectedAction.Type());
            InputManager.Instance.CurrentState = inputState;

            SelectedActionChangedEvent?.Invoke(this, EventArgs.Empty);
        }

    }

    private void InitializeOnAwake()
    {
        TargetList = new List<Unit>();
    }

    public void SetSingleTarget(Unit target)
    {
        if (TargetList.Contains(target))
        {
            return;
        }

        TargetList.Clear();
        TargetList.Add(target);
        SelectedTargetsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetTargetList(List<Unit> targets)
    {
        TargetList.Clear();
        TargetList.AddRange(targets);
        SelectedTargetsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void ClearTargetList()
    {
        TargetList.Clear();
        SelectedTargetsChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void StartSelectedAction()
    {
        // if(selectedAction.ValidateArguments(TargetList))
        InputManager.Instance.CurrentState = InputManager.State.Blocked;
        selectedAction.StartAction(TargetList, InternalOnActionCompleted);
    }

    // summary
    // this function is being provided as a callback for an action to inform the Action Manager 
    // that the action has finished
    private void InternalOnActionCompleted()
    {
        InputManager.Instance.CurrentState = InputManager.State.SelectUnitAndAction;
        ActionCompletedEvent?.Invoke(this, EventArgs.Empty);
    }

    // summary
    // this function maps the input state based on the declared Action Type
    private InputManager.State GetInputStateBasedOnActionType(BaseAction.ActionType actionType)
    {
        switch (actionType)
        {
            case BaseAction.ActionType.SingleEnemyTarget:
                return InputManager.State.SelectSingleEnemyTarget;
            case BaseAction.ActionType.MultipleEnemyTargets:
                return InputManager.State.SelectMultipleEnemyTargets;
            case BaseAction.ActionType.AllEnemyTargets:
                return InputManager.State.SelectAllEnemyTargets;
        }

        //default value;
        return InputManager.State.SelectUnitAndAction;

    }
}
