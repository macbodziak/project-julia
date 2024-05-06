using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageTakenEventArgs : EventArgs
{
    public int Damage { get; private set; }
    public bool IsKillingBlow { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageType Type { get; private set; }

    public DamageTakenEventArgs(int damage, DamageType damageType, bool isCritical, bool isKillingBlow)
    {
        Damage = damage;
        IsKillingBlow = isKillingBlow;
        IsCritical = isCritical;
        Type = damageType;
    }
}
