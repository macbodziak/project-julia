using System;
using UnityEngine;

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
        unit.TakeDamage(data.DamageAmount, data.Type, false, false);
    }

    public override bool IsAppliedEachTurn() { return true; }
}