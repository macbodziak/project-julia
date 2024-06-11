using UnityEngine;

[CreateAssetMenu(fileName = "Divine Protection Status Effect", menuName = "Scriptable Objects/Status Effects/Divine Protection Status Effect Preset", order = 3)]
public class DivineProtectionStatusEffect : StatusEffect, IAppliedEachTurn
{
    [SerializeField] private int minHealingAmount = 1;
    [SerializeField] private int maxHealingAmount = 2;
    [SerializeField] private int allSavingThrowModifier = 10;
    [SerializeField] private int allDamageResitanceModifier = 10;

    public int MinHealingAmount { get => minHealingAmount; private set => minHealingAmount = value; }
    public int MaxHealingAmount { get => maxHealingAmount; private set => maxHealingAmount = value; }
    public override StatusEffectType Type { get { return StatusEffectType.DivineProtection; } }

    public override void OnStart(Unit effectedUnit)
    {
        // provide healing
        base.OnStart(effectedUnit);
        HealingInfo healing = new HealingInfo(minHealingAmount, maxHealingAmount);
        unit.combatStats.ReceiveHealing(healing);

        //improve saving throws
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Agility] += allSavingThrowModifier;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Fortitude] += allSavingThrowModifier;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Willpower] += allSavingThrowModifier;

        //improve damage resistance to all damage types
        int length = System.Enum.GetNames(typeof(DamageType)).Length;
        for (int i = 0; i < length; i++)
        {
            unit.combatStats.damageResistanceModifiers[i] += allDamageResitanceModifier;
        }
    }

    public override void OnEnd()
    {
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Agility] -= allSavingThrowModifier;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Fortitude] -= allSavingThrowModifier;
        unit.combatStats.savingThrowsModifiers[(int)SavingThrowType.Willpower] -= allSavingThrowModifier;

        //improve damage resistance to all damage types
        int length = System.Enum.GetNames(typeof(DamageType)).Length;
        for (int i = 0; i < length; i++)
        {
            unit.combatStats.damageResistanceModifiers[i] -= allDamageResitanceModifier;
        }
    }

    public void ApplyEffect()
    {
        //provide healing each turn
        HealingInfo healing = new HealingInfo(minHealingAmount, maxHealingAmount);
        unit.combatStats.ReceiveHealing(healing);
    }
}