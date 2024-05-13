using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Attack Action Definition", menuName = "Scriptable Objects/Actions/Attack Action Definition", order = 1)]
public class AttackActionDefinition : ActionDefinition, ICanAttack
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
        CombatStats combatStats = actingUnit.GetComponent<CombatStats>();
        AttackInfo attack = GetAttackInfo(combatStats);
        foreach (Unit target in targets)
        {
            bool hit = target.combatStats.ReceiveAttack(attack);

            if (hit)
            {
                // PlayVisualEffect(VisualEffectOnHitPrefab, target.transform.position + new Vector3(0f, 1.2f, 0f));
                VisualEffect vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);
                if (vfx != null)
                {
                    Destroy(vfx.gameObject, 2.5f);
                }
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
