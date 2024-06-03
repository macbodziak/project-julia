using UnityEngine;

[CreateAssetMenu(fileName = "Stunned Status Effect", menuName = "Scriptable Objects/Status Effects/Stunned Status Effect Preset", order = 10)]
public class StunnedStatusEffect : StatusEffect
{
    public override bool IsActive { get { return false; } }

    public override StatusEffectType Type { get { return StatusEffectType.Stunned; } }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.CurrentActionPoints = 0;
        unit.combatStats.NoActionPointsRefresh++;
        unit.combatStats.DodgeModifier -= 10;
    }

    public override void OnEnd()
    {
        unit.combatStats.NoActionPointsRefresh--;
        unit.combatStats.DodgeModifier += 10;
    }
}