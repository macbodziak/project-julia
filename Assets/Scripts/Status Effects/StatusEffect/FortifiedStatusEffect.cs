using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortified Status Effect", menuName = "Scriptable Objects/Status Effects/Fortified Status Effect Preset", order = 3)]
public class FortifiedStatusEffect : StatusEffect
{
    [SerializeField] private int physicalDamageResistance = -10;

    public int PhysicalDamageResistance { get => physicalDamageResistance; private set => physicalDamageResistance = value; }

    public override bool IsActive { get { return false; } }

    public override StatusEffectType Type { get { return StatusEffectType.Fortified; } }


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
