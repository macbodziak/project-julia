using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionManager : MonoBehaviour
{
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    private static ActionManager _instance;
    [SerializeField] Unit selectedUnit;
    [SerializeField] BaseAction selectedAction;

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
        }
    }

    void SetSelectedUnit(Unit newSelectedUnit)
    {
        //Unit Unselected
        if (newSelectedUnit == null)
        {
            if (selectedUnit != null)
            {
                selectedUnit.SetSelectionVisual(false);
                selectedUnit = null;
                OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
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
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

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

    void SetSelectedAction(BaseAction newSelectedAction)
    {
        //Action Unselected
        if (newSelectedAction == null)
        {
            if (selectedAction != null)
            {
                // selectedAction.SetSelectionVisual(false);
                selectedAction = null;
                OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
            }
            return;
        }

        //Clicked already selected Action
        if (newSelectedAction == selectedAction)
        {
            return;
        }

        //Change Action selection
        if (selectedAction != null)
        {
            // selectedAction.SetSelectionVisual(false);
        }
        selectedAction = newSelectedAction;
        // selectedAction.SetSelectionVisual(true);
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);

    }

    public void TestingSetFirstAction()
    {
        if (selectedUnit == null)
        {
            return;
        }

        List<BaseAction> actions = selectedUnit.GetActionList();
        if (actions.Count == 0)
        {
            Debug.Log("no actions");
            return;
        }
        SetSelectedAction(actions[0]);
        selectedAction.StartAction();
    }
}
