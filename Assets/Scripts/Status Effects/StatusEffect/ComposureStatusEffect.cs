using UnityEngine;

[CreateAssetMenu(fileName = "Composure Status Effect", menuName = "Scriptable Objects/Status Effects/Composure Status Effect Preset", order = 10)]
public class ComposureStatusEffect : StatusEffect
{
    public override bool IsActive { get { return false; } }

    public override StatusEffectType Type { get { return StatusEffectType.Composure; } }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Fortitude] += 33;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Agility] += 33;
    }

    public override void OnEnd()
    {
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Fortitude] -= 33;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Agility] -= 33;
    }
}