using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    private int m_turnNumber;
    private bool m_isPlayerTurn;

    private Unit lastSelectedUnit;
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

    public event EventHandler NewPlayerTurnEvent;

    private void Start()
    {
        IsPlayerTurn = true;
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
            NewPlayerTurnEvent?.Invoke(this, EventArgs.Empty);
        }
    }


    private void StartEnemyTurn()
    {
        //implement
        //perform enemy AI

        Debug.Log("Enemy turn started...");
        EndTurn();
    }

    private void StartPalyerTurn()
    {
        ResetUnitSelection();
        ResetPlayerActionPoints();
    }

    private void HandleSelectedUnitChanged(object sender, EventArgs e)
    {
        lastSelectedUnit = ActionManager.Instance.SelectedUnit;
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
        ActionManager.Instance.SelectedUnit = lastSelectedUnit;
        ActionManager.Instance.SelectedAction = null;
        InputManager.Instance.CurrentState = InputManager.State.SelectUnitAndAction;
    }

    private void OnDestroy()
    {
        if (ActionManager.Instance != null)
        {
            ActionManager.Instance.SelectedUnitChangedEvent -= HandleSelectedUnitChanged;
        }
    }
}
