using UnityEngine;

[CreateAssetMenu(fileName = "Demonic Wrath Status Effect", menuName = "Scriptable Objects/Status Effects/Demonic Wrath Status Effect Preset", order = 10)]
public class DemonicWrathStatusEffect : StatusEffect
{
    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] private int minDamageAmount = 1;
    [SerializeField] private int maxDamageAmount = 1;
    [SerializeField] private DamageType _damageType = DamageType.Fire;
    [SerializeField] private int critChanceModifier = 1;

    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.DemonicWrath; } }

    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public int MinDamageAmount { get => minDamageAmount; set => minDamageAmount = value; }
    public int MaxDamageAmount { get => maxDamageAmount; set => maxDamageAmount = value; }
    public DamageType damageType { get => _damageType; set => _damageType = value; }
    public int CritChanceModifier { get => critChanceModifier; set => critChanceModifier = value; }

    public override void OnEnd()
    {
        unit.combatStats.DamageMultiplier -= DamageMultiplier;
        unit.combatStats.CritChanceModifier -= CritChanceModifier;
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.DamageMultiplier += DamageMultiplier;
        unit.combatStats.CritChanceModifier += CritChanceModifier;
    }

    public override void ApplyEffect()
    {
        int finalDamage = UnityEngine.Random.Range(minDamageAmount, maxDamageAmount);
        unit.combatStats.TakeDamage(finalDamage, damageType, false, false);
    }
}