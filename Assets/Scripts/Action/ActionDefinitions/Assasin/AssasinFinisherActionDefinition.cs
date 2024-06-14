using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Assasin Finisher Action Definition", menuName = "Scriptable Objects/Actions/Assasin/Assasin Finisher Action Definition", order = 200)]

//<summary>
//if target has one of the listed status effects or has HP below 25%, the crit chance goes up to 50% (+45 from the starting 5%)
//if this is a killing blow, AP gets refunded
//</summary>
//
public class AssasinFinisherActionDefinition : ActionDefinition, ICanAttack
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int hitChance;
    [SerializeField] private int critChance;
    [SerializeField] private DamageType damageType;


    public int MinDamage { get => minDamage; protected set => minDamage = value; }
    public int MaxDamage { get => maxDamage; protected set => maxDamage = value; }
    public int HitChance { get => hitChance; protected set => hitChance = value; }
    public int CritChance { get => critChance; protected set => critChance = value; }
    public DamageType DamageType { get => damageType; protected set => damageType = value; }


    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            Attack attack = GetAttackData(actingUnit, target);
            AttackResult attackResult = target.combatStats.ReceiveAttack(attack, actingUnit);
            if (attackResult.Hit)
            {
                // PlayVisualEffect(VisualEffectOnHitPrefab, target.transform.position + new Vector3(0f, 1.2f, 0f));
                GameObject vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);

                if (attackResult.IsKillingBlow)
                {
                    actingUnit.combatStats.ResetActionPoints();
                }
            }
        }
    }


    public Attack GetAttackData(Unit attacker, Unit target)
    {
        float attackerDamageModifier = Mathf.Clamp(attacker.combatStats.DamageMultiplier, 0f, 5f);

        float totalResitance = target.combatStats.GetTotalDamageResistance(damageType);
        totalResitance = Mathf.Clamp(totalResitance, -100, 100);
        float targetResistanceModifier = (100f - totalResitance) / 100f;

        int _minDamage = (int)(MinDamage * attackerDamageModifier * targetResistanceModifier);
        int _maxDamage = (int)(MaxDamage * attackerDamageModifier * targetResistanceModifier);

        int _hitChance = HitChance + attacker.combatStats.HitChanceModifier - target.combatStats.TotalDodge;
        _hitChance = Mathf.Clamp(_hitChance, 0, 95);

        int _critChance = CritChance + attacker.combatStats.CritChanceModifier;

        if (target.statusEffectController.HasStatusEffect(StatusEffectType.Stunned) ||
        target.statusEffectController.HasStatusEffect(StatusEffectType.Blinded) ||
        target.statusEffectController.HasStatusEffect(StatusEffectType.Marked) ||
        target.combatStats.CurrentHealthPoints / target.combatStats.MaxHealthPoints < 0.25f
        )
        {
            _critChance += 45;
        }

        _critChance = Mathf.Clamp(_critChance, 0, 95);
        Debug.Log("total crit chance = " + _critChance);
        return new Attack(_minDamage, _maxDamage, _hitChance, _critChance, DamageType);
    }


    public DamageType GetDamageType()
    {
        return DamageType;
    }

}
