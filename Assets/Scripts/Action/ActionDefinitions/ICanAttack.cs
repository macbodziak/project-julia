using UnityEngine;

public interface ICanAttack
{
    public Attack GetAttackData(Unit attacker, Unit target);

    public DamageType DamageType { get; }
}