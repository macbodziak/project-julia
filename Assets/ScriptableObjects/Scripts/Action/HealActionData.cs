using UnityEngine;

[CreateAssetMenu(fileName = "Heal Config Data", menuName = "Scriptable Objects/Heal Config Data", order = 3)]
public class HealActionData : BaseActionData
{
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;


    public int MinAmount { get => minAmount; protected set => minAmount = value; }
    public int MaxAmount { get => maxAmount; protected set => maxAmount = value; }

    public HealingInfo GetHealingInfo()
    {
        return new HealingInfo(MinAmount, MaxAmount);
    }
}
