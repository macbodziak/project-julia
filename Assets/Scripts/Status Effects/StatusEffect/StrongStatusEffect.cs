using UnityEngine;

[CreateAssetMenu(fileName = "Strong Status Effect", menuName = "Scriptable Objects/Status Effects/Strong Status Effect Preset", order = 3)]
public class StrongStatusEffect : StatusEffect
{
    [SerializeField] private float damageMultiplier = 1.1f;

    public float DamageMultiplier { get => damageMultiplier; private set => damageMultiplier = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Strong; } }

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