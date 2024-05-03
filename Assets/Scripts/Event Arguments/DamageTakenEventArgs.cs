using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageTakenEventArgs : EventArgs
{
    public int Damage { get; private set; }
    public bool IsKillingBlow { get; private set; }
    public bool IsCritical { get; private set; }

    public DamageTakenEventArgs(int damage, bool isCritical, bool isKillingBlow)
    {
        Damage = damage;
        IsKillingBlow = isKillingBlow;
        IsCritical = isCritical;
    }
}
