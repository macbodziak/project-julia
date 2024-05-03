using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HealingInfo
{
    public int MinAmount;
    public int MaxAmount;

    public HealingInfo(int minAmount, int maxAmount)
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
    }
}
