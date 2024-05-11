using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Action Definition", menuName = "Scriptable Objects/Actions/Heal Action Definition", order = 100)]
public class HealActionDefinition : ActionDefinition
{
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;

    public int MinAmount { get => minAmount; protected set => minAmount = value; }
    public int MaxAmount { get => maxAmount; protected set => maxAmount = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            target.combatStats.ReceiveHealing(GetHealingInfo());
        }
    }

    public HealingInfo GetHealingInfo()
    {
        return new HealingInfo(MinAmount, MaxAmount);
    }
}
