using UnityEngine;

[CreateAssetMenu(fileName = "Increase Damage Status Effect", menuName = "Scriptable Objects/Status Effects/Increase Damage Status Effect Preset", order = 3)]
public class IncreasedDamageStatusEffect : StatusEffect
{
    [SerializeField] private float damageMultiplier = 1.1f;

    public float DamageMultiplier { get => damageMultiplier; private set => damageMultiplier = value; }
    public override bool IsActive { get { return false; } }
    public override StatusEffectType Type { get { return StatusEffectType.Rage; } }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.DamageMultiplier += DamageMultiplier;
    }

    public override void OnEnd()
    {
        unit.combatStats.DamageMultiplier -= DamageMultiplier;
    }
}