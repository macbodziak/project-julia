using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UnityEditor;
using UnityEngine;

// <summary>
// This class handles the status effects of the given unit
// </summary>
public class StatusEffectController : MonoBehaviour
{
    [SerializeField] const float TIME_BETWEEN_STATUS_EFFECTS = 0.5f;
    [SerializeField] private List<StatusEffectBehaviour> statusEffectsBehaviours;
    private bool isProcessing = false;

    public bool IsProcessing { get => isProcessing; private set => isProcessing = value; }

    public static event EventHandler<StatusEffect> AnyUnitImmuneToStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitSavedFromStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitReceivedStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitRemovedStatusEffectEvent;


    private void Awake()
    {
        statusEffectsBehaviours = new();
    }


    // <summary>
    // This method gets called in the Turn Manager before the units turn 
    // </summary>
    public void ApplyActiveStatusEffects()
    {
        StartCoroutine(ApplyActiveStatusEffectsCoroutine());
    }


    private IEnumerator ApplyActiveStatusEffectsCoroutine()
    {
        IsProcessing = true;
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffectsBehaviours.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffectsBehaviours[i].IsActive() == true)
            {
                statusEffectsBehaviours[i].ApplyEffect();
                yield return new WaitForSeconds(TIME_BETWEEN_STATUS_EFFECTS);

                statusEffectsBehaviours[i].Decrement();

                //remove status effect from list if expired
                if (statusEffectsBehaviours[i].RemainingDuration <= 0)
                {
                    AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffectsBehaviours[i].statusEffect);
                    Destroy(statusEffectsBehaviours[i]);
                    statusEffectsBehaviours.RemoveAt(i);
                }
            }
        }
        IsProcessing = false;
        yield return null;
    }


    public void ApplyPassiveStatusEffects()
    {
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffectsBehaviours.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffectsBehaviours[i].IsActive() == false)
            {
                statusEffectsBehaviours[i].Decrement();

                //remove status effect from list if expired
                if (statusEffectsBehaviours[i].RemainingDuration <= 0)
                {
                    AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffectsBehaviours[i].statusEffect);
                    Destroy(statusEffectsBehaviours[i]);
                    statusEffectsBehaviours.RemoveAt(i);
                }
            }
        }
    }


    public List<StatusEffectBehaviour> GetStatusEffects()
    {
        return statusEffectsBehaviours;
    }


    private StatusEffectBehaviour GetStatusEffectBehaviour(StatusEffect statusEffectPreset)
    {
        foreach (StatusEffectBehaviour item in statusEffectsBehaviours)
        {
            if (item.statusEffectType == statusEffectPreset.Type)
            {
                return item;
            }
        }
        return null;
    }


    private void ReceiveStatusEffect(StatusEffect statusEffectPreset)
    {
        StatusEffectBehaviour statusEffectBehaviour;

        //check if Status Effect is already applied
        statusEffectBehaviour = GetStatusEffectBehaviour(statusEffectPreset);
        if (statusEffectBehaviour != null)
        {
            statusEffectBehaviour.ResetDuration();
        }
        else
        {
            //add new status effect
            statusEffectBehaviour = gameObject.AddComponent<StatusEffectBehaviour>();
            statusEffectBehaviour.Initialize(statusEffectPreset);
            statusEffectsBehaviours.Add(statusEffectBehaviour);
            AnyUnitReceivedStatusEffectEvent?.Invoke(this, statusEffectPreset);
        }
    }


    public void RemoveStatusEffect(StatusEffect statusEffectPreset)
    {
        StatusEffectBehaviour statusEffectBehaviour = GetStatusEffectBehaviour(statusEffectPreset);
        if (statusEffectBehaviour != null)
        {
            AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffectPreset);
            Destroy(statusEffectBehaviour);
            statusEffectsBehaviours.Remove(statusEffectBehaviour);
        }
    }


    public bool TryReceivingStatusEffect(StatusEffect statusEffectPreset)
    {
        CombatStats combatStats = GetComponent<CombatStats>();
        int requiredSavingThrow = 100 - combatStats.GetStatusEffectSaveValue(statusEffectPreset.Type);
        if (requiredSavingThrow <= 0)
        {
            AnyUnitImmuneToStatusEffectEvent?.Invoke(this, statusEffectPreset);
            return false;
        }
        else
        {
            int savingThrow = UnityEngine.Random.Range(0, 100);
            if (savingThrow > requiredSavingThrow)
            {
                AnyUnitSavedFromStatusEffectEvent?.Invoke(this, statusEffectPreset);
                return true;
            }
            else
            {
                ReceiveStatusEffect(statusEffectPreset);
                return false;
            }
        }
    }


    public bool HasStatusEffect(StatusEffectType type)
    {
        foreach (StatusEffectBehaviour item in statusEffectsBehaviours)
        {
            if (item.statusEffectType == type)
            {
                return true;
            }
        }
        return false;
    }


    public void Clear()
    {
        foreach (StatusEffectBehaviour statusEffect in statusEffectsBehaviours)
        {
            Destroy(statusEffect);
        }

        statusEffectsBehaviours.Clear();
        StopAllCoroutines();
        isProcessing = false;
    }


    public int Count()
    {
        return statusEffectsBehaviours.Count;
    }
}
