using UnityEngine;

[CreateAssetMenu(fileName = "Attack Config Data", menuName = "Scriptable Objects/Attack Config Data", order = 1)]
public class AttackActionData : BaseActionData
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

    public AttackInfo GetAttackInfo(CombatStats combatStats = null)
    {
        if (combatStats == null)
        {
            return new AttackInfo(MinDamage, MaxDamage, HitChance, CritChance, DamageType);
        }
        else
        {
            int _minDamage = (int)(MinDamage * combatStats.DamageMultiplier);
            int _maxDamage = (int)(MaxDamage * combatStats.DamageMultiplier);
            int _hitChance = HitChance + combatStats.HitChanceModifier;
            int _critChance = CritChance + combatStats.CritChanceModifier;
            return new AttackInfo(_minDamage, _maxDamage, _hitChance, _critChance, DamageType);
        }
    }
}
