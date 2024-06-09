using UnityEngine;

[CreateAssetMenu(fileName = "Demons Mark Status Effect", menuName = "Scriptable Objects/Status Effects/Demon sMark Status Effect Preset", order = 3)]
public class DemonsMarkStatusEffect : StatusEffect
{
    [SerializeField] private int physicalDamageResistance = -10;
    [SerializeField] private int fireDamageResistance = -10;


    public int PhysicalDamageResistance { get => physicalDamageResistance; private set => physicalDamageResistance = value; }
    public int FireDamageResistance { get => fireDamageResistance; private set => fireDamageResistance = value; }


    public override StatusEffectType Type { get { return StatusEffectType.DemonsMark; } }


    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Physical] += physicalDamageResistance;
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Fire] += fireDamageResistance;
    }

    public override void OnEnd()
    {
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Physical] -= physicalDamageResistance;
        unit.combatStats.damageResistanceModifiers[(int)DamageType.Fire] -= fireDamageResistance;
    }
}
