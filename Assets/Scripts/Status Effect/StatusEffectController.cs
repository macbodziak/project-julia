using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UnityEditor;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    [SerializeField] private List<StatusEffect> statusEffects;

    private void Awake()
    {
        statusEffects = new();
    }
    public void ApplyStatusEffects()
    {
        //cycle backwards, becasue we might need to remove elements while iterating over list
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            //if the status effect needs to be apllied each turn
            if (statusEffects[i].IsAppliedEachTurn() == true)
            {
                statusEffects[i].ApplyEffect(OnStatusEffectApplied);
            }

            statusEffects[i].Decrement();

            //remove status effect from list if expired
            if (statusEffects[i].RemainingDuration == 0)
            {
                Destroy(statusEffects[i]);
                statusEffects.RemoveAt(i);
            }
        }
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
            //TO DO remove from list
            Destroy(statusEffect);
        }
    }

    private void OnStatusEffectApplied()
    {
        Debug.Log("status effect applied... moving on to the next");
    }
}
