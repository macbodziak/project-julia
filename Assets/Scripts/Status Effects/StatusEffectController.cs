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
    [SerializeField] private List<StatusEffectBehaviour> earlyStatusEffectsBehaviours;
    [SerializeField] private List<StatusEffectBehaviour> statusEffectsBehaviours;
    private bool isProcessing = false;

    public bool IsProcessing { get => isProcessing; private set => isProcessing = value; }

    public static event EventHandler<StatusEffect> AnyUnitImmuneToStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitSavedFromStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitReceivedStatusEffectEvent;
    public static event EventHandler<StatusEffect> AnyUnitRemovingStatusEffectEvent;
    //this is a general event thta something has changed (and displays need to be refreshed for example)
    public static event EventHandler AnyUnitStatusEffectsChangedEvent;


    private void Awake()
    {
        earlyStatusEffectsBehaviours = new();
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
        //TO DO - refactor
        IsProcessing = true;

        //first go through early list
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = earlyStatusEffectsBehaviours.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (earlyStatusEffectsBehaviours[i].IsActive() == true)
            {
                earlyStatusEffectsBehaviours[i].ApplyEffect();
                yield return new WaitForSeconds(TIME_BETWEEN_STATUS_EFFECTS);

                earlyStatusEffectsBehaviours[i].Decrement();

                //remove status effect from list if expired
                if (earlyStatusEffectsBehaviours[i].RemainingDuration <= 0)
                {
                    AnyUnitRemovingStatusEffectEvent?.Invoke(this, earlyStatusEffectsBehaviours[i].statusEffect);
                    Destroy(earlyStatusEffectsBehaviours[i]);
                    earlyStatusEffectsBehaviours.RemoveAt(i);
                    AnyUnitStatusEffectsChangedEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        //after early list go through regular list
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
                    AnyUnitRemovingStatusEffectEvent?.Invoke(this, statusEffectsBehaviours[i].statusEffect);
                    Destroy(statusEffectsBehaviours[i]);
                    statusEffectsBehaviours.RemoveAt(i);
                    AnyUnitStatusEffectsChangedEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        IsProcessing = false;
        yield return null;
    }


    public void ApplyPassiveStatusEffects()
    {
        ProccessPassiveStatusEffectsFromList(earlyStatusEffectsBehaviours);
        ProccessPassiveStatusEffectsFromList(statusEffectsBehaviours);
    }

    private void ProccessPassiveStatusEffectsFromList(List<StatusEffectBehaviour> statusEffectList)
    {
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffectList.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffectList[i].IsActive() == false)
            {
                statusEffectList[i].Decrement();

                //remove status effect from list if expired
                if (statusEffectList[i].RemainingDuration <= 0)
                {
                    AnyUnitRemovingStatusEffectEvent?.Invoke(this, statusEffectList[i].statusEffect);
                    Destroy(statusEffectList[i]);
                    statusEffectList.RemoveAt(i);
                    AnyUnitStatusEffectsChangedEvent?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public List<StatusEffectBehaviour> GetStatusEffects()
    {
        List<StatusEffectBehaviour> returnList = new();
        returnList.AddRange(earlyStatusEffectsBehaviours);
        returnList.AddRange(statusEffectsBehaviours);
        return returnList;
    }


    private StatusEffectBehaviour GetStatusEffectBehaviour(StatusEffect statusEffectPreset)
    {
        //first check the ealry list
        foreach (StatusEffectBehaviour item in earlyStatusEffectsBehaviours)
        {
            if (item.statusEffectType == statusEffectPreset.Type)
            {
                return item;
            }
        }

        //if not found in early list, check regular list
        foreach (StatusEffectBehaviour item in statusEffectsBehaviours)
        {
            if (item.statusEffectType == statusEffectPreset.Type)
            {
                return item;
            }
        }
        return null;
    }


    public void ReceiveStatusEffect(StatusEffect statusEffectPreset, int duration)
    {
        StatusEffectBehaviour statusEffectBehaviour;

        //check if Status Effect is already applied
        statusEffectBehaviour = GetStatusEffectBehaviour(statusEffectPreset);
        if (statusEffectBehaviour != null)
        {
            if (statusEffectBehaviour.RemainingDuration < duration)
            {
                statusEffectBehaviour.ResetDuration(duration);
            }
        }
        else
        {
            //add new status effect
            statusEffectBehaviour = gameObject.AddComponent<StatusEffectBehaviour>();
            statusEffectBehaviour.Initialize(statusEffectPreset, duration);
            //check if it should be execute earlier than the regular effects
            if (statusEffectPreset.ExecuteEarly)
            {
                earlyStatusEffectsBehaviours.Add(statusEffectBehaviour);
            }
            else
            {
                statusEffectsBehaviours.Add(statusEffectBehaviour);
            }
            AnyUnitReceivedStatusEffectEvent?.Invoke(this, statusEffectPreset);
            AnyUnitStatusEffectsChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }


    public void RemoveStatusEffect(StatusEffect statusEffectPreset)
    {
        StatusEffectBehaviour statusEffectBehaviour = GetStatusEffectBehaviour(statusEffectPreset);
        if (statusEffectBehaviour != null)
        {
            AnyUnitRemovingStatusEffectEvent?.Invoke(this, statusEffectPreset);
            Destroy(statusEffectBehaviour);
            if (statusEffectPreset.ExecuteEarly)
            {
                earlyStatusEffectsBehaviours.Remove(statusEffectBehaviour);
            }
            else
            {
                statusEffectsBehaviours.Remove(statusEffectBehaviour);
            }
            AnyUnitStatusEffectsChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }


    public bool TryReceivingStatusEffect(StatusEffectDurationInfo statusEffectInfo)
    {
        CombatStats combatStats = GetComponent<CombatStats>();
        if (combatStats.GetStatusEffectImmunity(statusEffectInfo.statusEffect.Type))
        {
            Debug.Log("<color=#5555ff>Immune to </color>" + statusEffectInfo.statusEffect.Name);
            AnyUnitImmuneToStatusEffectEvent?.Invoke(this, statusEffectInfo.statusEffect);
            return false;
        }

        SavingThrowType savingThrowType = statusEffectInfo.statusEffect.savingThrowType;
        if (savingThrowType != SavingThrowType.None)
        {
            int requiredSavingThrow = 100 - combatStats.GetTotalSavingThrowValue(savingThrowType);
            int savingThrow = UnityEngine.Random.Range(0, 100);
            Debug.Log("<color=#5555ff>Making Saving Throw </color>" + savingThrow + " > " + requiredSavingThrow + " ?");
            if (savingThrow > requiredSavingThrow)
            {
                AnyUnitSavedFromStatusEffectEvent?.Invoke(this, statusEffectInfo.statusEffect);
                return true;
            }
        }

        ReceiveStatusEffect(statusEffectInfo.statusEffect, statusEffectInfo.duration);
        return false;

    }


    public bool HasStatusEffect(StatusEffectType type)
    {
        foreach (StatusEffectBehaviour item in earlyStatusEffectsBehaviours)
        {
            if (item.statusEffectType == type)
            {
                return true;
            }
        }
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
        foreach (StatusEffectBehaviour statusEffect in earlyStatusEffectsBehaviours)
        {
            Destroy(statusEffect);
        }
        foreach (StatusEffectBehaviour statusEffect in statusEffectsBehaviours)
        {
            Destroy(statusEffect);
        }
        earlyStatusEffectsBehaviours.Clear();
        statusEffectsBehaviours.Clear();
        StopAllCoroutines();
        isProcessing = false;
    }


    public int Count()
    {
        return earlyStatusEffectsBehaviours.Count + statusEffectsBehaviours.Count;
    }



    public static void ClearAllListeners()
    {
        AnyUnitImmuneToStatusEffectEvent = null;
        AnyUnitSavedFromStatusEffectEvent = null;
        AnyUnitReceivedStatusEffectEvent = null;
        AnyUnitRemovingStatusEffectEvent = null;
        AnyUnitStatusEffectsChangedEvent = null;
    }
}
