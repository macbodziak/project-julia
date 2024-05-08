using UnityEngine;

// <summary
// Decrease Amount of action points
// </summary>
public class RageStatusEffect : StatusEffect
{
    // [SerializeField] SlowStatusEffectData data;

    protected void Awake()
    {
        baseData = LoadData<RageStatusEffectData>("StatusEffects/Rage Status Effect Data");
    }

    protected override void Start()
    {
        RageStatusEffectData data = baseData as RageStatusEffectData;
        base.Start();
        unit.combatStats.DamageMultiplier += data.DamageMultiplier;
        unit.combatStats.HitChanceModifier += data.HitChanceModifier;
        unit.combatStats.CritChanceModifier += data.CritChanceModifier;

    }

    protected void OnDestroy()
    {
        RageStatusEffectData data = baseData as RageStatusEffectData;
        unit.combatStats.DamageMultiplier -= data.DamageMultiplier;
        unit.combatStats.HitChanceModifier -= data.HitChanceModifier;
        unit.combatStats.CritChanceModifier -= data.CritChanceModifier;

    }
}