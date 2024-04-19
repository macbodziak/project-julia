using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] AbilityLayoutController abilityLayoutController;

    static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

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
    private void Start()
    {
        ActionManager.Instance.OnSelectedUnitChanged += handleSelectedUnitChanged;
    }

    public void handleSelectedUnitChanged(object Sender, EventArgs eventArgs)
    {
        abilityLayoutController.ClearList();
        Unit selectedUnit = ActionManager.Instance.SelectedUnit;
        if (selectedUnit != null)
        {
            List<BaseAction> actionList = selectedUnit.GetActionList();
            abilityLayoutController.CreateAndShowAbilityList(actionList);
        }
    }

    public void handleSelectedActionChanged()
    {

    }

    private void OnDestroy()
    {
        if (ActionManager.Instance != null)
        {
            ActionManager.Instance.OnSelectedUnitChanged -= handleSelectedUnitChanged;
        }
    }
}
