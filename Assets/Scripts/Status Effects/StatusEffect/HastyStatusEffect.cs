using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Hasty Status Effect", menuName = "Scriptable Objects/Status Effects/Hasty Status Effect Preset", order = 10)]
public class HastyStatusEffect : StatusEffect
{
    [SerializeField] private int actionPointModifier = 1;
    [SerializeField] private int hitChanceModifier = 10;
    [SerializeField] private int dodgeModifier = 10;

    public int ActionPointModifier { get => actionPointModifier; private set => actionPointModifier = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Hasty; } }


    public override void OnEnd()
    {
        unit.combatStats.ActionPointsModifier -= actionPointModifier;
        unit.combatStats.HitChanceModifier -= hitChanceModifier;
        unit.combatStats.DodgeModifier -= dodgeModifier;
    }


    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.ActionPointsModifier += actionPointModifier;
        unit.combatStats.HitChanceModifier += hitChanceModifier;
        unit.combatStats.DodgeModifier += dodgeModifier;
        unit.statusEffectController.RemoveStatusEffect(StatusEffectType.Slowed);
    }
}