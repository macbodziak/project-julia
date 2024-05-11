using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System;

[RequireComponent(typeof(CombatStats), typeof(StatusEffectController))]
public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<ActionBehaviour> actionList;
    [SerializeField] bool isPlayer;

    public static event EventHandler OnMouseEnterAnyUnit;
    public static event EventHandler OnMouseExitAnyUnit;

    private CombatStats _combatStats;
    private StatusEffectController _statusEffectController;

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


    private void Start()
    {
        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<ActionBehaviour>(actionList);
        _combatStats = GetComponent<CombatStats>();
        _statusEffectController = GetComponent<StatusEffectController>();
    }

    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<ActionBehaviour> GetActionList()
    {
        return actionList;
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
