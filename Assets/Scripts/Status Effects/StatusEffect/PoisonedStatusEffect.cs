using UnityEngine;

[CreateAssetMenu(fileName = "Poisoned Status Effect", menuName = "Scriptable Objects/Status Effects/Poisoned Status Effect Preset", order = 10)]
public class PoisonedStatusEffect : StatusEffect, IAppliedEachTurn
{
    [SerializeField] private int minDamageAmount = 1;
    [SerializeField] private int maxDamageAmount = 2;
    [SerializeField] private DamageType _damageType = DamageType.Poison;
    // [SerializeField] private ParticleSystem m_particleSystem;

    public int MinDamageAmount { get => minDamageAmount; private set => minDamageAmount = value; }
    public int MaxDamageAmount { get => maxDamageAmount; private set => maxDamageAmount = value; }
    public DamageType damageType { get => _damageType; private set => _damageType = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Poisoned; } }

    public override void OnEnd()
    {

    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
    }

    public void ApplyEffect()
    {
        int damageAmount = UnityEngine.Random.Range(minDamageAmount, maxDamageAmount);
        unit.combatStats.TakeDamage(damageAmount, damageType, false, false);
    }
}