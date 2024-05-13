using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    private int m_turnNumber;
    private bool m_isPlayerTurn;

    private static TurnManager _instance;

    public int TurnNumber
    {
        get { return m_turnNumber; }
        private set { m_turnNumber = value; }
    }

    public bool IsPlayerTurn
    {
        get { return m_isPlayerTurn; }
        private set { m_isPlayerTurn = value; }
    }

    public static TurnManager Instance { get { return _instance; } }

    public event EventHandler TurnEndedEvent;


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
        IsPlayerTurn = true;
        TurnNumber = 1;
    }

    public void EndTurn()
    {
        if (IsPlayerTurn)
        {
            ProcessPassiveStatusEffects(CombatEncounterManager.Instance.GetPlayerUnitList());
            IsPlayerTurn = false;
            StartCoroutine(StartEnemyTurn());
        }
        else
        {
            ProcessPassiveStatusEffects(CombatEncounterManager.Instance.GetEnemyUnitList());
            IsPlayerTurn = true;
            StartCoroutine(StartPlayerTurn());
            TurnNumber++;
        }
        TurnEndedEvent?.Invoke(this, EventArgs.Empty);
    }

    //<summary>
    // get a list containing status controller from all processed unit
    // invoke apply status for each
    // return the list, so it can be used to verify if all status controllers have finished processing
    //</summary>
    private List<StatusEffectController> ProcessActiveStatusEffects(List<Unit> units)
    {
        List<StatusEffectController> statusEffectControllers = new();
        //iterating from the end becuase units might get removed while iterating if they die
        for (int i = units.Count - 1; i >= 0; i--)
        {
            StatusEffectController statusEffectController = units[i].GetComponent<StatusEffectController>();
            statusEffectControllers.Add(statusEffectController);
            statusEffectController.ApplyActiveStatusEffects();
        }
        return statusEffectControllers;
    }

    private void ProcessPassiveStatusEffects(List<Unit> units)
    {
        //iterating from the end becuase units might get removed while iterating if they die
        for (int i = units.Count - 1; i >= 0; i--)
        {
            StatusEffectController statusEffectController = units[i].GetComponent<StatusEffectController>();
            statusEffectController.ApplyPassiveStatusEffects();
        }
    }

    private IEnumerator StartEnemyTurn()
    {
        DisableInputAndUnitSelection();
        List<StatusEffectController> statusControllers = ProcessActiveStatusEffects(CombatEncounterManager.Instance.GetEnemyUnitList());
        while (HasStatusEffectProcessingCompleted(statusControllers) == false)
        {
            yield return new WaitForSeconds(0.12f);
        }
        ResetEnemyActionPoints();
        EnemyAIManager.Instance.StartEnemyTurn();
        yield return null;
    }

    private IEnumerator StartPlayerTurn()
    {
        List<StatusEffectController> statusControllers = ProcessActiveStatusEffects(CombatEncounterManager.Instance.GetPlayerUnitList());
        while (HasStatusEffectProcessingCompleted(statusControllers) == false)
        {
            yield return new WaitForSeconds(0.12f);
        }
        if (CombatEncounterManager.Instance.IsEncounterOver == false)
        {
            TickCooldownCounter(CombatEncounterManager.Instance.GetPlayerUnitList());
            ResetPlayerActionPoints();
            ResetUnitSelection();
        }
        yield return null;
    }

    private void TickCooldownCounter(List<Unit> units)
    {
        foreach (Unit unit in units)
        {
            //iterate through all action behaviours
            List<ActionBehaviour> actions = unit.GetActionList();
            foreach (ActionBehaviour action in actions)
            {
                //decrements cooldown if applicable
                action.DecrementCooldown();
            }
        }
    }

    private void ResetPlayerActionPoints()
    {
        List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
        foreach (Unit unit in playerUnits)
        {
            unit.combatStats.ResetActionPoints();
        }
    }

    private void ResetEnemyActionPoints()
    {
        List<Unit> enemyUnits = CombatEncounterManager.Instance.GetEnemyUnitList();
        foreach (Unit unit in enemyUnits)
        {
            unit.combatStats.ResetActionPoints();
        }
    }

    private void ResetUnitSelection()
    {
        ActionManager.Instance.SelectedUnit = null;
        ActionManager.Instance.SelectedAction = null;
        InputManager.Instance.SetState(InputState.SelectUnitAndAction);
    }

    private void DisableInputAndUnitSelection()
    {
        ActionManager.Instance.SelectedUnit = null;
        ActionManager.Instance.SelectedAction = null;
        InputManager.Instance.SetState(InputState.Blocked);
    }
    private void OnDestroy()
    {

    }

    private bool HasStatusEffectProcessingCompleted(List<StatusEffectController> statusEffectControllers)
    {
        foreach (var controller in statusEffectControllers)
        {
            if (controller.IsProcessing)
            {
                return false;
            }
        }

        return true;
    }
}
