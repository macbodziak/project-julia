public interface ICanAttack
{
    public Attack GetAttackData(CombatStats combatStatsModifier = null);
    public DamageType DamageType { get; }
}