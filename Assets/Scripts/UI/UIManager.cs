using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] AbilityLayoutController abilityLayoutController;
    [SerializeField] TextMeshProUGUI StateText;

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
            InitializationOnAwake();
        }
    }
    private void Start()
    {
        ActionManager.Instance.OnSelectedUnitChanged += HandleSelectedUnitChanged;
        InputManager.Instance.OnInputStateChanged += HandleInputStateChanged;
    }

    private void HandleSelectedUnitChanged(object Sender, EventArgs eventArgs)
    {
        abilityLayoutController.ClearList();
        Unit selectedUnit = ActionManager.Instance.SelectedUnit;
        if (selectedUnit != null)
        {
            List<BaseAction> actionList = selectedUnit.GetActionList();
            abilityLayoutController.CreateAndShowAbilityList(actionList);
        }
    }

    private void OnDestroy()
    {
        if (ActionManager.Instance != null)
        {
            ActionManager.Instance.OnSelectedUnitChanged -= HandleSelectedUnitChanged;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInputStateChanged -= HandleInputStateChanged;
        }
    }

    private void HandleInputStateChanged(object Sender, EventArgs eventArgs)
    {
        StateText.text = "Input State" + InputManager.Instance.GetInputState();
    }

    private void InitializationOnAwake()
    {
        StateText.text = "Input State" + InputManager.Instance.GetInputState();
    }
}
