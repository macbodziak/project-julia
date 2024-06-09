using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Slow Status Effect", menuName = "Scriptable Objects/Status Effects/Slow Status Effect Preset", order = 10)]
public class SlowStatusEffect : StatusEffect
{
    [SerializeField] private int actionPointModifier = -1;

    public int ActionPointModifier { get => actionPointModifier; private set => actionPointModifier = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Slowed; } }


    public override void OnEnd()
    {
        unit.combatStats.ActionPointsModifier -= actionPointModifier;
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.ActionPointsModifier += actionPointModifier;
    }
}