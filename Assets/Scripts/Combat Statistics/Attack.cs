using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Attack
{
    public int MinDamage;
    public int MaxDamage;
    public int HitChance;
    public int CritChance;
    public DamageType Type;

    public Attack(int minDamage, int maxDamage, int hitChance, int critChance, DamageType type)
    {
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        HitChance = hitChance;
        CritChance = critChance;
        Type = type;
    }
}
