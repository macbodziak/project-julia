using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Agile Status Effect", menuName = "Scriptable Objects/Status Effects/Agile Status Effect Preset", order = 3)]

public class AgileStatusEffect : StatusEffect
{
    [SerializeField] private int dodgeModifier = 10;

    public int DodgeModifier { get => dodgeModifier; private set => dodgeModifier = value; }

    public override bool IsActive { get { return false; } }

    public override StatusEffectType Type { get { return StatusEffectType.Agile; } }


    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.DodgeModifier += dodgeModifier;
    }

    public override void OnEnd()
    {
        unit.combatStats.DodgeModifier -= dodgeModifier;
    }
}
