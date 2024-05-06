using System;
using UnityEngine;

public class BleedingStatusEffect : StatusEffect
{
    [SerializeField] BleedingStatusEffectData data;

    protected void Awake()
    {
        data = LoadData<BleedingStatusEffectData>("StatusEffects/Bleeding Status Effect Data");
    }

    public override void ApplyEffect(Action onCompletedcallback)
    {
        Debug.Log($"applying status effect: {data.Name} with {data.DamageAmount} damage");
        unit.TakeDamage(data.DamageAmount, false);
    }

    public override bool IsAppliedEachTurn() { return true; }
}