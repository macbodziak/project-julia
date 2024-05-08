using UnityEngine;

// <summary
// Decrease Amount of action points
// </summary>
public class SlowedStatusEffect : StatusEffect
{
    // [SerializeField] SlowStatusEffectData data;

    protected void Awake()
    {
        baseData = LoadData<SlowStatusEffectData>("StatusEffects/Slow Status Effect Data");
    }

    protected override void Start()
    {
        SlowStatusEffectData data = baseData as SlowStatusEffectData;
        base.Start();
        unit.combatStats.ActionPointsModifier += data.ActionPointModifier;
    }

    protected void OnDestroy()
    {
        SlowStatusEffectData data = baseData as SlowStatusEffectData;
        unit.combatStats.ActionPointsModifier -= data.ActionPointModifier;
    }
}