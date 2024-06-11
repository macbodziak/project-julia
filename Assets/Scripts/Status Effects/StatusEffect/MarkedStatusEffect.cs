using UnityEngine;

[CreateAssetMenu(fileName = "Marked Status Effect", menuName = "Scriptable Objects/Status Effects/Marked Status Effect Preset", order = 3)]
public class MarkedStatusEffect : StatusEffect
{
    [SerializeField] private int dodgeModifier = -10;


    public override StatusEffectType Type { get { return StatusEffectType.Marked; } }


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
