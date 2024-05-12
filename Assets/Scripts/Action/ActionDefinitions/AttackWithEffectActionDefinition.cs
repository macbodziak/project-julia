using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Attack with Status Effect Action Definition", menuName = "Scriptable Objects/Actions/Attack with Effect Action Definition", order = 2)]
public class AttackWithEffectActionDefinition : ActionDefinition
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int hitChance;
    [SerializeField] private int critChance;
    [SerializeField] private DamageType damageType;
    [SerializeField] private List<StatusEffect> _statusEffects;

    public int MinDamage { get => minDamage; protected set => minDamage = value; }
    public int MaxDamage { get => maxDamage; protected set => maxDamage = value; }
    public int HitChance { get => hitChance; protected set => hitChance = value; }
    public int CritChance { get => critChance; protected set => critChance = value; }
    public DamageType DamageType { get => damageType; protected set => damageType = value; }
    public List<StatusEffect> StatusEffectsApplied { get => _statusEffects; protected set => _statusEffects = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        CombatStats combatStats = actingUnit.GetComponent<CombatStats>();
        AttackInfo attack = GetAttackInfo(combatStats);
        foreach (Unit target in targets)
        {
            bool hit = target.combatStats.ReceiveAttack(attack);

            if (hit && target.combatStats.CurrentHealthPoints > 0)
            {
                ApplyStatusEffects(target, StatusEffectsApplied);
                // PlayVisualEffect(VisualEffectOnHitPrefab, target.transform.position + new Vector3(0f, 1.2f, 0f));
                VisualEffect vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);
                Destroy(vfx, 2.5f);
            }
        }
    }

    public AttackInfo GetAttackInfo(CombatStats combatStatsModifier = null)
    {
        if (combatStatsModifier == null)
        {
            return new AttackInfo(MinDamage, MaxDamage, HitChance, CritChance, DamageType);
        }
        else
        {
            int _minDamage = (int)(MinDamage * combatStatsModifier.DamageMultiplier);
            int _maxDamage = (int)(MaxDamage * combatStatsModifier.DamageMultiplier);
            int _hitChance = HitChance + combatStatsModifier.HitChanceModifier;
            int _critChance = CritChance + combatStatsModifier.CritChanceModifier;
            return new AttackInfo(_minDamage, _maxDamage, _hitChance, _critChance, DamageType);
        }
    }
}