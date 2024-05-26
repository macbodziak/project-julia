public interface ICanAttack
{
    public AttackInfo GetAttackInfo(CombatStats combatStatsModifier = null);
    public DamageType DamageType { get; }
}