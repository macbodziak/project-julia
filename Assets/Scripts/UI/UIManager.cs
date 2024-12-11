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
    [SerializeField] private TextMeshProUGUI actionNameText;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI endTurnText;
    [SerializeField] private TextMeshProUGUI currentTurnPlayerText;
    [SerializeField] private EncounterOverScreen encounterOverScreen;
    [SerializeField] private GameObject HUD;
    [SerializeField] private UnitInspector unitInspector;
    [SerializeField] private UnitOverviewPanel unitOverviewPanel;
    [SerializeField] private SelectedUnitInspectorPanel selectedUnitPanel;


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
        //todo - to implement proper quitting
        quitButton.onClick.AddListener(() => Application.Quit());

        //register with event publishers
        ActionManager.Instance.SelectedUnitChangedEvent += HandleSelectedUnitChanged;
        ActionManager.Instance.SelectedActionChangedEvent += HandleSelectedActionChanged;
        ActionManager.Instance.ActionCompletedEvent += HandleActionCompleted;
        InputManager.Instance.InputStateChangedEvent += HandleInputStateChanged;
        TurnManager.Instance.TurnEndedEvent += HandleTurnEnded;
        GameManagement.EncounterManager.Instance.EncounterOverEvent += HandleEncounterOver;
        GameManagement.EncounterManager.Instance.EncounterSetupCompleteEvent += HandleEncounterSetupComplete;
        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;
        CombatStats.AnyUnitTookDamageEvent += HandleHealthBarUpdate;
        CombatStats.AnyUnitReceivedHealingEvent += HandleHealthBarUpdate;
        CombatStats.AnyUnitActionPointsChangedEvent += HandleAnyUnitActionPointsChanged;
        StatusEffectController.AnyUnitStatusEffectsChangedEvent += HanldeStatusEffectInfoPanelUpdate;
        PortraitBehavior.AnyPortraitMouseEnterEvent += HandleAnyPortraitMouseEnter;
        PortraitBehavior.AnyPortraitMouseExitEvent += HandleAnyPortraitMouseExit;
        ActionButton.MouseEnterAnyActionButtonEvent += HandleMouseEnterAnyActionButton;
        ActionButton.MouseExitAnyActionButtonEvent += HandleMouseExitAnyActionButton;
    }


    private void HandleSelectedActionChanged(object sender, EventArgs e)
    {
        if (ActionManager.Instance.SelectedAction == null)
        {
            actionNameText.text = "";
        }
        else
        {
            actionNameText.text = ActionManager.Instance.SelectedAction.Name;
        }
    }


    public void HandleMouseEnterAnyActionButton(object sender, EventArgs e)
    {
        ActionButton actionButton = sender as ActionButton;
        if (actionButton == null)
        {
            return;
        }
        actionNameText.text = actionButton.action.Name;
    }


    public void HandleMouseExitAnyActionButton(object sender, EventArgs e)
    {

        HandleSelectedActionChanged(sender, e);
    }


    private void HandleAnyPortraitMouseExit(object sender, EventArgs e)
    {
        unitInspector.Hide();
    }


    private void HandleAnyPortraitMouseEnter(object sender, EventArgs e)
    {
        PortraitBehavior portrait = sender as PortraitBehavior;
        ShowUnitInspector(portrait.unit);
    }


    private void HandleEncounterSetupComplete(object sender, EventArgs e)
    {
        unitOverviewPanel.Setup(
            GameManagement.EncounterManager.Instance.GetPlayerUnitList(),
            GameManagement.EncounterManager.Instance.GetEnemyUnitList()
            );

    }


    private void HandleSelectedUnitChanged(object Sender, EventArgs eventArgs)
    {
        UpdateAbilityList();
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
            // abilityLayoutController.ClearList();
            currentTurnPlayerText.text = "Enemy Turn";
        }
        else
        {
            // endTurnButton.interactable = true will be set when Turn manager changes input state 
            // from Blacked and the OnBlockedInputStateExit() method will be called
            endTurnText.text = "Turn " + TurnManager.Instance.TurnNumber;
            currentTurnPlayerText.text = "Players Turn";
            UpdateAbilityList();
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
            ActionManager.Instance.SelectedActionChangedEvent -= HandleSelectedActionChanged;
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

        if (GameManagement.EncounterManager.Instance != null)
        {
            GameManagement.EncounterManager.Instance.EncounterOverEvent -= HandleEncounterOver;
            GameManagement.EncounterManager.Instance.EncounterSetupCompleteEvent -= HandleEncounterSetupComplete;
        }

        //static events so no null check needed
        Unit.OnMouseEnterAnyUnit -= HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit -= HandleMouseExitAnyUnit;
        CombatStats.AnyUnitTookDamageEvent -= HandleHealthBarUpdate;
        CombatStats.AnyUnitReceivedHealingEvent -= HandleHealthBarUpdate;
        CombatStats.AnyUnitActionPointsChangedEvent -= HandleAnyUnitActionPointsChanged;
        StatusEffectController.AnyUnitStatusEffectsChangedEvent -= HanldeStatusEffectInfoPanelUpdate;
        PortraitBehavior.AnyPortraitMouseEnterEvent -= HandleAnyPortraitMouseEnter;
        PortraitBehavior.AnyPortraitMouseExitEvent -= HandleAnyPortraitMouseExit;
        ActionButton.MouseEnterAnyActionButtonEvent -= HandleMouseEnterAnyActionButton;
        ActionButton.MouseExitAnyActionButtonEvent -= HandleMouseExitAnyActionButton;
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
        if (playerWon)
        {
            GetComponent<UISoundPlayer>().PlayWonSound();
            encounterOverScreen.Show(playerWon);
        }
        else
        {
            GetComponent<UISoundPlayer>().PlayLostSound();
            encounterOverScreen.Show(playerWon);
        }
        yield return null;
    }


    private void HandleMouseEnterAnyUnit(object sender, EventArgs eventArgs)
    {
        Unit unit = sender as Unit;
        if (unit != null)
        {
            ShowUnitInspector(unit);
        }
    }


    private void HandleMouseExitAnyUnit(object sender, EventArgs eventArgs)
    {
        unitInspector.Hide();
    }


    private void HandleHealthBarUpdate(object sender, EventArgs eventArgs)
    {
        CombatStats cs = sender as CombatStats;
        Unit senderUnit = cs.GetComponent<Unit>();
        if (senderUnit == unitInspector.unit)
        {
            unitInspector.UpdateStats(cs);
        }

        if (senderUnit == selectedUnitPanel.unit)
        {
            selectedUnitPanel.UpdateHealthBar(senderUnit);
        }
    }


    private void HanldeStatusEffectInfoPanelUpdate(object sender, EventArgs eventArgs)
    {
        StatusEffectController ctrl = sender as StatusEffectController;
        Unit senderUnit = ctrl.GetComponent<Unit>();
        if (senderUnit == unitInspector.unit)
        {
            unitInspector.UpdateStatusEffects(ctrl);
        }
    }


    private void HandleAnyUnitActionPointsChanged(object sender, EventArgs e)
    {
        CombatStats cs = sender as CombatStats;
        Unit senderUnit = cs.GetComponent<Unit>();

        if (senderUnit == selectedUnitPanel.unit)
        {
            selectedUnitPanel.UpdateActionPoints(senderUnit);
        }
    }


    private void ShowUnitInspector(Unit unit)
    {
        unitInspector.Setup(unit, true);
        unitInspector.Show();
    }


    private void UpdateAbilityList()
    {
        abilityLayoutController.ClearList();
        Unit selectedUnit = ActionManager.Instance.SelectedUnit;
        if (selectedUnit != null)
        {
            List<ActionBehaviour> actionList = selectedUnit.GetActionList();
            abilityLayoutController.CreateAndShowAbilityList(actionList);

            selectedUnitPanel.Show();
            selectedUnitPanel.Setup(selectedUnit);

        }
        else
        {
            selectedUnitPanel.Hide();
        }
    }
}
