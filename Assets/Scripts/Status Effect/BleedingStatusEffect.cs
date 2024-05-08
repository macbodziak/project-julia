using System;
using UnityEngine;

// <summary
// apply physical damage over time
// </summary>

public class BleedingStatusEffect : StatusEffect
{
    [SerializeField] BleedingStatusEffectData data;

    protected void Awake()
    {
        data = LoadData<BleedingStatusEffectData>("StatusEffects/Bleeding Status Effect Data");
        baseData = data;
    }

    public override void ApplyEffect()
    {
        unit.combatStats.TakeDamage(data.DamageAmount, data.Type, false, false);
    }

    public override bool IsActive() { return true; }
}