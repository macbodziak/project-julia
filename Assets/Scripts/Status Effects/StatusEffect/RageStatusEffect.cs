using UnityEngine;

[CreateAssetMenu(fileName = "Rage Status Effect", menuName = "Scriptable Objects/Status Effects/Rage Status Effect Preset", order = 10)]
public class RageStatusEffect : StatusEffect
{
    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] private int hitChanceModifier = 1;
    [SerializeField] private int critChanceModifier = 1;

    public float DamageMultiplier { get => damageMultiplier; protected set => damageMultiplier = value; }
    public int HitChanceModifier { get => hitChanceModifier; protected set => hitChanceModifier = value; }
    public int CritChanceModifier { get => critChanceModifier; protected set => critChanceModifier = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Rage; } }

    public override void OnEnd()
    {
        unit.combatStats.DamageMultiplier -= damageMultiplier;
        unit.combatStats.HitChanceModifier -= hitChanceModifier;
        unit.combatStats.CritChanceModifier -= critChanceModifier;
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.DamageMultiplier += damageMultiplier;
        unit.combatStats.HitChanceModifier += hitChanceModifier;
        unit.combatStats.CritChanceModifier += critChanceModifier;
    }

}