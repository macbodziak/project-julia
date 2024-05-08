
// <summary
// Decrease Amount of action points
// </summary>
public class IncreasedDamageStatusEffect : StatusEffect
{
    protected void Awake()
    {
        baseData = LoadData<IncreasedDamageEffectData>("StatusEffects/Increased Damage Status Effect Data");
    }

    protected override void Start()
    {
        IncreasedDamageEffectData data = baseData as IncreasedDamageEffectData;
        base.Start();
        unit.combatStats.DamageMultiplier += data.DamageMultiplier;
    }

    protected void OnDestroy()
    {
        IncreasedDamageEffectData data = baseData as IncreasedDamageEffectData;
        unit.combatStats.DamageMultiplier -= data.DamageMultiplier;
    }
}