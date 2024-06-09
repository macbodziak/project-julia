using UnityEngine;

[CreateAssetMenu(fileName = "Exposed Weakness Status Effect", menuName = "Scriptable Objects/Status Effects/Exposed WeaknessStatus Effect Preset", order = 3)]
public class ExposedWeaknessStatusEffect : StatusEffect
{
    [SerializeField] private int physicalDamageResistance = -10;

    public int PhysicalDamageResistance { get => physicalDamageResistance; private set => physicalDamageResistance = value; }


    public override StatusEffectType Type { get { return StatusEffectType.ExposedWeakness; } }


    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Physical] += physicalDamageResistance;
    }

    public override void OnEnd()
    {
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Physical] -= physicalDamageResistance;
    }
}
