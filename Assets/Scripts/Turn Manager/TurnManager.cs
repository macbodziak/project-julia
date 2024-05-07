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
            IsPlayerTurn = false;
            StartCoroutine(StartEnemyTurn());
        }
        else
        {
            IsPlayerTurn = true;
            StartCoroutine(StartPlayerTurn());
            TurnNumber++;
        }
        TurnEndedEvent?.Invoke(this, EventArgs.Empty);
    }

    private List<StatusEffectController> ProcessStatusEffects(List<Unit> units)
    {
        List<StatusEffectController> statusEffectControllers = new();
        //iterating from the end becuase units might get removed while iterating if they die
        for (int i = units.Count - 1; i >= 0; i--)
        {
            StatusEffectController statusEffectController = units[i].GetComponent<StatusEffectController>();
            statusEffectControllers.Add(statusEffectController);
            statusEffectController.ApplyStatusEffects();
        }
        return statusEffectControllers;
    }

    private IEnumerator StartEnemyTurn()
    {
        DisableInputAndUnitSelection();
        List<StatusEffectController> statusControllers = ProcessStatusEffects(CombatEncounterManager.Instance.GetEnemyUnitList());
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
        List<StatusEffectController> statusControllers = ProcessStatusEffects(CombatEncounterManager.Instance.GetPlayerUnitList());
        while (HasStatusEffectProcessingCompleted(statusControllers) == false)
        {
            yield return new WaitForSeconds(0.12f);
        }
        if (CombatEncounterManager.Instance.IsEncounterOver == false)
        {
            ResetPlayerActionPoints();
            ResetUnitSelection();
        }
        yield return null;
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
