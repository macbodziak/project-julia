using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding Status Effect", menuName = "Scriptable Objects/Status Effects/Bleeding Status Effect Preset", order = 10)]
public class BleedingStatusEffect : StatusEffect
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private DamageType _damageType = DamageType.Physical;
    // [SerializeField] private ParticleSystem m_particleSystem;

    public int DamageAmount { get => damageAmount; private set => damageAmount = value; }
    public DamageType damageType { get => _damageType; private set => _damageType = value; }
    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.Bleeding; } }

    public override void OnEnd()
    {

    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
    }

    public override void ApplyEffect()
    {
        unit.combatStats.TakeDamage(damageAmount, damageType, false, false);
    }
}