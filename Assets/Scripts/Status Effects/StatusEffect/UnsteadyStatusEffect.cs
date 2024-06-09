using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unsteady Status Effect", menuName = "Scriptable Objects/Status Effects/Unsteady Status Effect Preset", order = 3)]
public class UnsteadyStatusEffect : StatusEffect
{
    [SerializeField] private int dodgeModifier = -10;

    public int DodgeModifier { get => dodgeModifier; private set => dodgeModifier = value; }

    public override StatusEffectType Type { get { return StatusEffectType.Unsteady; } }


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
