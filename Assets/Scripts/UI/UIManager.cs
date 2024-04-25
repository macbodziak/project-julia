using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField] AbilityLayoutController abilityLayoutController;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] Button endTurnButton;
    [SerializeField] TextMeshProUGUI endTurnText;

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
        stateText.text = "Input State" + InputManager.Instance.CurrentState;
        endTurnButton.onClick.AddListener(TurnManager.Instance.EndTurn);

        //register with event publishers
        ActionManager.Instance.SelectedUnitChangedEvent += HandleSelectedUnitChanged;
        ActionManager.Instance.ActionCompletedEvent += HandleActionCompleted;
        InputManager.Instance.InputStateChangedEvent += HandleInputStateChanged;
        TurnManager.Instance.TurnEndedEvent += HandleTurnEnded;
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



    private void HandleInputStateChanged(object Sender, EventArgs eventArgs)
    {
        stateText.text = "Input State" + InputManager.Instance.CurrentState;

        //if blocked, grey out all interactable elements
    }

    private void HandleTurnEnded(object Sender, EventArgs eventArgs)
    {
        if (TurnManager.Instance.IsPlayerTurn == false)
        {
            endTurnButton.interactable = false;
            abilityLayoutController.ClearList();
        }
        else
        {
            endTurnButton.interactable = true;
            endTurnText.text = "Turn " + TurnManager.Instance.TurnNumber;
        }
    }

    private void HandleActionCompleted(object Sender, EventArgs eventArgs)
    {
        abilityLayoutController.RefreshAbilityList();
    }

    private void OnDestroy()
    {
        if (ActionManager.Instance != null)
        {
            ActionManager.Instance.SelectedUnitChangedEvent -= HandleSelectedUnitChanged;
            ActionManager.Instance.ActionCompletedEvent -= HandleActionCompleted;
        }
        if (InputManager.Instance != null)
        {
            InputManager.Instance.InputStateChangedEvent -= HandleInputStateChanged;
        }
        if (TurnManager.Instance != null)
        {
            TurnManager.Instance.TurnEndedEvent -= HandleTurnEnded;
        }
    }

}
