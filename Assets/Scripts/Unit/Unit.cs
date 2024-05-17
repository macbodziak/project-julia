
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] bool isPlayer;

    public static event EventHandler OnMouseEnterAnyUnit;
    public static event EventHandler OnMouseExitAnyUnit;

    private CombatStats _combatStats;
    private StatusEffectController _statusEffectController;
    private ActionController _actionController;

    public CombatStats combatStats
    {
        get { return _combatStats; }
    }

    public StatusEffectController statusEffectController
    {
        get { return _statusEffectController; }
    }

    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    public int CurrentHealthPoints
    {
        get { return _combatStats.CurrentHealthPoints; }
    }

    public int ActionPoints
    {
        get { return _combatStats.CurrentActionPoints; }
    }

    public int PowerPoints
    {
        get { return _combatStats.CurrentPowerPoints; }
    }


    private void Start()
    {
        selectedVisual = GetComponent<SelectedVisual>();
        _combatStats = GetComponent<CombatStats>();
        _statusEffectController = GetComponent<StatusEffectController>();
        _actionController = GetComponent<ActionController>();
    }

    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<ActionBehaviour> GetActionList()
    {
        return _actionController.GetActionList();
    }

    public void TryReceivingStatusEffect(StatusEffect statusEffect)
    {
        _statusEffectController.TryReceivingStatusEffect(statusEffect);
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        _statusEffectController.RemoveStatusEffect(statusEffect);
    }

    private void OnMouseEnter()
    {
        OnMouseEnterAnyUnit?.Invoke(this, EventArgs.Empty);
    }

    private void OnMouseExit()
    {
        OnMouseExitAnyUnit?.Invoke(this, EventArgs.Empty);
    }
}