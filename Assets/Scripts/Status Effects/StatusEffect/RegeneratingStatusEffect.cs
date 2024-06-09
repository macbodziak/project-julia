using UnityEngine;

[CreateAssetMenu(fileName = "Regenerating Status Effect", menuName = "Scriptable Objects/Status Effects/Regenerating Status Effect Preset", order = 10)]
public class RegeneratingStatusEffect : StatusEffect, IAppliedEachTurn
{
    [SerializeField] private int minHealingAmount = 1;
    [SerializeField] private int maxHealingAmount = 2;

    public int MinHealingAmount { get => minHealingAmount; private set => minHealingAmount = value; }
    public int MaxHealingAmount { get => maxHealingAmount; private set => maxHealingAmount = value; }
    public override StatusEffectType Type { get { return StatusEffectType.Regenerating; } }

    public override void OnEnd()
    {

    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        HealingInfo healing = new HealingInfo(minHealingAmount, maxHealingAmount);
        unit.combatStats.ReceiveHealing(healing);
    }

    public void ApplyEffect()
    {
        HealingInfo healing = new HealingInfo(minHealingAmount, maxHealingAmount);
        unit.combatStats.ReceiveHealing(healing);
    }
}