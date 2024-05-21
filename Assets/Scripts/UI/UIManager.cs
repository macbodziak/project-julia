using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    [SerializeField] private AbilityLayoutController abilityLayoutController;
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI endTurnText;
    [SerializeField] private TextMeshProUGUI currentTurnPlayerText;
    [SerializeField] private EncounterOverScreen encounterOverScreen;
    [SerializeField] private GameObject HUD;
    [SerializeField] private UnitInfoPanel unitInfoPanel;


    bool isInputBlocked;

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
        isInputBlocked = false;

        stateText.text = "Input State" + InputManager.Instance.CurrentState;
        endTurnButton.onClick.AddListener(TurnManager.Instance.EndTurn);

        //register with event publishers
        ActionManager.Instance.SelectedUnitChangedEvent += HandleSelectedUnitChanged;
        ActionManager.Instance.ActionCompletedEvent += HandleActionCompleted;
        InputManager.Instance.InputStateChangedEvent += HandleInputStateChanged;
        TurnManager.Instance.TurnEndedEvent += HandleTurnEnded;
        CombatEncounterManager.Instance.EncounterOverEvent += HandleEncounterOver;
        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;
    }


    private void HandleSelectedUnitChanged(object Sender, EventArgs eventArgs)
    {
        abilityLayoutController.ClearList();
        Unit selectedUnit = ActionManager.Instance.SelectedUnit;
        if (selectedUnit != null)
        {
            List<ActionBehaviour> actionList = selectedUnit.GetActionList();
            abilityLayoutController.CreateAndShowAbilityList(actionList);
        }
    }


    private void HandleInputStateChanged(object Sender, EventArgs eventArgs)
    {
        stateText.text = "Input State" + InputManager.Instance.CurrentState;

        //if blocked, grey out all interactable elements
        if (InputManager.Instance.CurrentState == InputState.Blocked)
        {
            isInputBlocked = true;
            OnBlockedInputStateEnter();
        }
        else if (isInputBlocked == true)
        {
            isInputBlocked = false;
            OnBlockedInputStateExit();
        }
    }


    private void OnBlockedInputStateEnter()
    {
        abilityLayoutController.SetInteractable(false);
        endTurnButton.interactable = false;
    }


    private void OnBlockedInputStateExit()
    {
        abilityLayoutController.SetInteractable(true);
        endTurnButton.interactable = true;
    }


    private void HandleTurnEnded(object Sender, EventArgs eventArgs)
    {
        if (TurnManager.Instance.IsPlayerTurn == false)
        {
            // endTurnButton.interactable = false;
            abilityLayoutController.ClearList();
            currentTurnPlayerText.text = "Enemy Turn";

        }
        else
        {
            // endTurnButton.interactable = true will be set when Turn manager changes input state 
            // from Blacked and the OnBlockedInputStateExit() method will be called
            endTurnText.text = "Turn " + TurnManager.Instance.TurnNumber;
            currentTurnPlayerText.text = "Players Turn";
        }
    }


    private void HandleActionCompleted(object Sender, EventArgs eventArgs)
    {
        //TO DO - remove, this is handles by exit blocked state anyway
        // abilityLayoutController.RefreshAbilityList();
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

        if (CombatEncounterManager.Instance != null)
        {
            CombatEncounterManager.Instance.EncounterOverEvent -= HandleEncounterOver;
        }

        //static events so no null check needed
        Unit.OnMouseEnterAnyUnit -= HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit -= HandleMouseExitAnyUnit;
    }


    private void HandleEncounterOver(object sender, EncounterOverEventArgs eventArgs)
    {
        IEnumerator coroutine = OnEncounterOverDelayed(eventArgs.PlayerWon, 2.1f);
        StartCoroutine(coroutine);
    }


    private IEnumerator OnEncounterOverDelayed(bool playerWon, float delay)
    {
        yield return new WaitForSeconds(delay);
        InputManager.Instance.SetState(InputState.EncounterOverScreen);
        HUD.SetActive(false);
        encounterOverScreen.Show(playerWon);
        yield return null;
    }


    private void HandleMouseEnterAnyUnit(object sender, EventArgs eventArgs)
    {
        if (InputManager.Instance.CurrentState != InputState.Blocked)
        {
            Unit unit = sender as Unit;
            unitInfoPanel.Setup(
                unit.Name,
                unit.statusEffectController.GetStatusEffects(),
                unit.combatStats.CurrentHealthPoints,
                unit.combatStats.MaxHealthPoints,
                true);
            unitInfoPanel.Show();
        }
    }


    private void HandleMouseExitAnyUnit(object sender, EventArgs eventArgs)
    {
        unitInfoPanel.Hide();
    }

}
