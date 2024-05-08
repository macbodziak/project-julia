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
    [SerializeField] private List<StatusEffect> statusEffects;
    private bool isProcessing = false;

    public bool IsProcessing { get => isProcessing; private set => isProcessing = value; }

    public static event EventHandler AnyUnitImmuneToStatusEffectEvent;
    public static event EventHandler AnyUnitSavedFromStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitReceivedStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitRemovedStatusEffectEvent;
    private void Awake()
    {
        statusEffects = new();
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
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffects[i].IsActive() == true)
            {
                statusEffects[i].ApplyEffect();
                yield return new WaitForSeconds(TIME_BETWEEN_STATUS_EFFECTS);

                statusEffects[i].Decrement();

                //remove status effect from list if expired
                if (statusEffects[i].RemainingDuration == 0)
                {
                    AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffects[i]);
                    Destroy(statusEffects[i]);
                    statusEffects.RemoveAt(i);
                }
            }
        }
        IsProcessing = false;
        yield return null;
    }

    public void ApplyPassiveStatusEffects()
    {
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffects[i].IsActive() == false)
            {
                statusEffects[i].Decrement();

                //remove status effect from list if expired
                if (statusEffects[i].RemainingDuration == 0)
                {
                    AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffects[i]);
                    Destroy(statusEffects[i]);
                    statusEffects.RemoveAt(i);
                }
            }
        }
    }

    public List<StatusEffect> GetStatusEffects()
    {
        return statusEffects;
    }

    private void ReceiveStatusEffect<T>() where T : StatusEffect
    {
        StatusEffect statusEffect;

        //check if Status Effect is already applied
        statusEffect = GetComponent<T>();
        if (statusEffect != null)
        {
            statusEffect.ResetDuration();
        }
        else
        {
            //add new status effect
            statusEffect = gameObject.AddComponent<T>();
            statusEffects.Add(statusEffect);
            AnyUnitReceivedStatusEffectEvent?.Invoke(this, statusEffect);
        }
    }

    private void ReceiveStatusEffect(StatusEffectType type)
    {
        switch (type)
        {
            case StatusEffectType.Bleeding:
                ReceiveStatusEffect<BleedingStatusEffect>();
                break;
            case StatusEffectType.Burning:
                ReceiveStatusEffect<BurningStatusEffect>();
                break;
            case StatusEffectType.Poisened:
                // ReceiveStatusEffect<BleedingStatusEffect>();
                break;
            case StatusEffectType.Slowed:
                ReceiveStatusEffect<SlowedStatusEffect>();
                break;
            case StatusEffectType.IncreasedDamage:
                ReceiveStatusEffect<IncreasedDamageStatusEffect>();
                break;
            case StatusEffectType.Rage:
                ReceiveStatusEffect<RageStatusEffect>();
                break;
        }
    }

    public void RemoveStatusEffect<T>() where T : StatusEffect
    {
        StatusEffect statusEffect = GetComponent<T>();
        if (statusEffect != null)
        {
            AnyUnitRemovedStatusEffectEvent?.Invoke(this, statusEffect);
            Destroy(statusEffect);
            statusEffects.Remove(statusEffect);
        }
    }

    public void RemoveStatusEffect(StatusEffectType type)
    {
        switch (type)
        {
            case StatusEffectType.Bleeding:
                RemoveStatusEffect<BleedingStatusEffect>();
                break;
            case StatusEffectType.Burning:
                RemoveStatusEffect<BurningStatusEffect>();
                break;
            case StatusEffectType.Poisened:
                // RemoveStatusEffect<BleedingStatusEffect>();
                break;
            case StatusEffectType.Slowed:
                RemoveStatusEffect<SlowedStatusEffect>();
                break;
            case StatusEffectType.IncreasedDamage:
                RemoveStatusEffect<IncreasedDamageStatusEffect>();
                break;
            case StatusEffectType.Rage:
                RemoveStatusEffect<RageStatusEffect>();
                break;
        }
    }

    public bool TryReceivingStatusEffect(StatusEffectType type)
    {
        CombatStats combatStats = GetComponent<CombatStats>();
        int requiredSavingThrow = 100 - combatStats.GetStatusEffectSaveValue(type);
        if (requiredSavingThrow <= 0)
        {
            AnyUnitImmuneToStatusEffectEvent?.Invoke(this, EventArgs.Empty);
            return false;
        }
        else
        {
            int savingThrow = UnityEngine.Random.Range(0, 100);
            if (savingThrow > requiredSavingThrow)
            {
                AnyUnitSavedFromStatusEffectEvent?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
            {
                ReceiveStatusEffect(type);
                return false;
            }
        }
    }
    public void Clear()
    {
        foreach (StatusEffect statusEffect in statusEffects)
        {
            Destroy(statusEffect);
        }

        statusEffects.Clear();
        StopAllCoroutines();
        isProcessing = false;
    }

    public int Count()
    {
        return statusEffects.Count;
    }
}
