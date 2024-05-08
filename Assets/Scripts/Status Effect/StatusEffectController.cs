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

    private void Awake()
    {
        statusEffects = new();
    }

    // <summary>
    // This method gets called in the Turn Manager before the units turn 
    // </summary>
    public void ApplyStatusEffects()
    {
        StartCoroutine(ApplyStatusEffectsCoroutine());
    }

    private IEnumerator ApplyStatusEffectsCoroutine()
    {
        IsProcessing = true;
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffects[i].IsAppliedEachTurn() == true)
            {
                statusEffects[i].ApplyEffect();
                yield return new WaitForSeconds(TIME_BETWEEN_STATUS_EFFECTS);
            }

            statusEffects[i].Decrement();

            //remove status effect from list if expired
            if (statusEffects[i].RemainingDuration == 0)
            {
                Destroy(statusEffects[i]);
                statusEffects.RemoveAt(i);
            }
        }
        IsProcessing = false;
        yield return null;
    }

    public List<StatusEffect> GetStatusEffects()
    {
        return statusEffects;
    }

    public void ReceiveStatusEffect<T>() where T : StatusEffect
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
        }
    }

    public void RemoveStatusEffect<T>() where T : StatusEffect
    {
        StatusEffect statusEffect = GetComponent<T>();
        if (statusEffect != null)
        {
            Destroy(statusEffect);
            statusEffects.Remove(statusEffect);
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
}
