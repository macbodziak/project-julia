using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inspired Status Effect", menuName = "Scriptable Objects/Status Effects/Inspired Status Effect Preset", order = 3)]
public class InspiredStatusEffect : StatusEffect
{
    [SerializeField] private int hitChanceModifier = 15;


    public override StatusEffectType Type { get { return StatusEffectType.Inspired; } }

    public int HitChanceModifier { get => hitChanceModifier; private set => hitChanceModifier = value; }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.HitChanceModifier += HitChanceModifier;
    }

    public override void OnEnd()
    {
        unit.combatStats.HitChanceModifier -= HitChanceModifier;
    }
}
