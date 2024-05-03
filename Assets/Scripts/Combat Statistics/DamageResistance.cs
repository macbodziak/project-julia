using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageResistance : MonoBehaviour
{
    [Range(-100, 100)] public int PhysicalResistance;
    [Range(-100, 100)] public int FireResistance;
    [Range(-100, 100)] public int IceResistance;
    [Range(-100, 100)] public int ElectricResistance;
    [Range(-100, 100)] public int PoisionResistance;

    public int GetResistanceValue(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                return PhysicalResistance;
            case DamageType.Fire:
                return FireResistance;
            case DamageType.Ice:
                return IceResistance;
            case DamageType.Electric:
                return ElectricResistance;
            case DamageType.Poision:
                return PoisionResistance;
            default:
                return 0;
        }
    }

    public int ApplyResistance(int damageAmount, DamageType damageType)
    {
        float modifier = (100f - GetResistanceValue(damageType)) / 100f;
        return (int)(damageAmount * modifier);
    }
}
