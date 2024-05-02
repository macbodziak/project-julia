using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    private int m_turnNumber;
    private bool m_isPlayerTurn;

    private static TurnManager _instance;

    // private Unit lastSelectedUnit;
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
        ActionManager.Instance.SelectedUnitChangedEvent += HandleSelectedUnitChanged;
    }

    public void EndTurn()
    {
        if (IsPlayerTurn)
        {
            IsPlayerTurn = false;
            StartEnemyTurn();
        }
        else
        {
            IsPlayerTurn = true;
            StartPalyerTurn();
            TurnNumber++;
        }
        TurnEndedEvent?.Invoke(this, EventArgs.Empty);
    }


    private void StartEnemyTurn()
    {
        DisableInputAndUnitSelection();
        ResetEnemyActionPoints();
        EnemyAIManager.Instance.StartEnemyTurn();
    }

    private void ResetEnemyActionPoints()
    {
        List<Unit> enemyUnits = CombatEncounterManager.Instance.GetEnemyUnitList();
        foreach (Unit unit in enemyUnits)
        {
            unit.ResetActionPoints();
        }
    }

    private void StartPalyerTurn()
    {
        ResetUnitSelection();
        ResetPlayerActionPoints();
    }

    private void HandleSelectedUnitChanged(object sender, EventArgs e)
    {
        //to do
    }

    private void ResetPlayerActionPoints()
    {
        List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
        foreach (Unit unit in playerUnits)
        {
            unit.ResetActionPoints();
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
        if (ActionManager.Instance != null)
        {
            ActionManager.Instance.SelectedUnitChangedEvent -= HandleSelectedUnitChanged;
        }
    }
}
