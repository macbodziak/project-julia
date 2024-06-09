using UnityEngine;

[CreateAssetMenu(fileName = "Concealed Status Effect", menuName = "Scriptable Objects/Status Effects/Concealed Status Effect Preset", order = 10)]
public class ConcealedStatusEffect : StatusEffect, IReactiveToDamageTaken
{
    [SerializeField] int dodgeModifier;
    [SerializeField] int hitChanceModifier;
    [SerializeField] int critChanceModifier;

    public override StatusEffectType Type { get { return StatusEffectType.Concealed; } }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        unit.combatStats.DodgeModifier += dodgeModifier;
        unit.combatStats.HitChanceModifier += hitChanceModifier;
        unit.combatStats.CritChanceModifier += critChanceModifier;

        //subscribe to unit combatstats to get notified if taken damage
        unit.combatStats.ThisUnitTookDamageEvent += OnDamageTaken;
    }

    public override void OnEnd()
    {
        unit.combatStats.DodgeModifier -= dodgeModifier;
        unit.combatStats.HitChanceModifier -= hitChanceModifier;
        unit.combatStats.CritChanceModifier -= critChanceModifier;

        unit.combatStats.ThisUnitTookDamageEvent -= OnDamageTaken;
    }


    public void OnDamageTaken(object sender, DamageTakenEventArgs args)
    {
        //notify StatusEffect Controller to Remove this status effect
        //this happens after the Update Loop, so it is safe to request Destory on Safe (I hope)
        unit.statusEffectController.RemoveStatusEffect(this);
    }
}