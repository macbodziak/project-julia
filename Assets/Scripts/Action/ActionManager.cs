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

    private Unit selectedUnit;
    private List<Unit> TargetList;
    private ActionBehaviour selectedAction;

    public Unit SelectedUnit
    {
        get { return selectedUnit; }
        set { SetSelectedUnit(value); }
    }

    public ActionBehaviour SelectedAction
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
        SelectedUnitChangedEvent?.Invoke(this, EventArgs.Empty);

    }

    public void ClearUnitSelection()
    {
        selectedUnit = null;
    }

    private void SetSelectedAction(ActionBehaviour newSelectedAction)
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

            InputState inputState = GetInputStateBasedOnActionType(selectedAction.targetingMode);
            InputManager.Instance.SetState(inputState);

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
        InputManager.Instance.SetState(InputState.Blocked);
        selectedAction.StartAction(TargetList, InternalOnActionCompleted);
    }

    // summary
    // this function is being provided as a callback for an action to inform the Action Manager 
    // that the action has finished
    private void InternalOnActionCompleted()
    {
        if (CombatEncounterManager.Instance.IsEncounterOver == false)
        {
            InputManager.Instance.SetState(InputState.SelectUnitAndAction);
        }
        ActionCompletedEvent?.Invoke(this, EventArgs.Empty);
    }

    // summary
    // this function maps the input state based on the declared Action Type
    private InputState GetInputStateBasedOnActionType(TargetingModeType actionType)
    {
        switch (actionType)
        {
            case TargetingModeType.SingleEnemyTarget:
                return InputState.SelectSingleEnemyTarget;

            case TargetingModeType.MultipleEnemyTargets:
                return InputState.SelectMultipleEnemyTargets;

            case TargetingModeType.AllEnemyTargets:
                return InputState.SelectAllEnemyTargets;

            case TargetingModeType.SingleAllyTarget:
                return InputState.SelectSingleAllyTarget;

            case TargetingModeType.AllAllyTargets:
                return InputState.SelectAllAllyTargets;

            case TargetingModeType.NoTarget:
                return InputState.SelectNoTarget;
        }

        //default value;
        return InputState.SelectUnitAndAction;

    }
}
