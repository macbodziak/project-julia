using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindedStatusEffect : StatusEffect
{
    [SerializeField] private int hitChanceModifier = 10;


    public override bool IsActive { get { return false; } }

    public override StatusEffectType Type { get { return StatusEffectType.Blinded; } }


    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.HitChanceModifier += hitChanceModifier;
    }

    public override void OnEnd()
    {
        unit.combatStats.HitChanceModifier -= hitChanceModifier;
    }
}
