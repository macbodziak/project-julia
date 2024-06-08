using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Burning Status Effect", menuName = "Scriptable Objects/Status Effects/Burning Status Effect Preset", order = 10)]
public class BurningStatusEffect : StatusEffect
{
    [SerializeField] private int minDamageAmount = 1;
    [SerializeField] private int maxDamageAmount = 2;
    [SerializeField] private int iceDamageResistanceModifier = 50;
    [SerializeField] private DamageType m_damageType = DamageType.Fire;

    public int MinDamageAmount { get => minDamageAmount; private set => minDamageAmount = value; }
    public int MaxDamageAmount { get => maxDamageAmount; private set => maxDamageAmount = value; }
    public DamageType damageType { get => m_damageType; private set => m_damageType = value; }
    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.Burning; } }

    public override void OnEnd()
    {
        base.OnEnd();
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Ice] -= iceDamageResistanceModifier; ;
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);

        unit.combatStats.damageResistanceModifiers[(int)DamageType.Ice] += iceDamageResistanceModifier;
    }

    public override void ApplyEffect()
    {
        int damageAmount = UnityEngine.Random.Range(minDamageAmount, maxDamageAmount);
        unit.combatStats.TakeDamage(damageAmount, damageType, false, false);
    }

}