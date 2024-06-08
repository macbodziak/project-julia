using System;

public class DodgedEventArgs : EventArgs
{
    public Unit Attacker { get; private set; }
    public DamageType Type { get; private set; }

    public DodgedEventArgs(Unit attacker, DamageType damageType)
    {
        Attacker = attacker;
        Type = damageType;
    }
}
