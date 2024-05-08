using System;

public class StatusEffectEventArgs : EventArgs
{
    public StatusEffect statusEffect;
    public StatusEffectEventArgs(StatusEffect _statusEffect)
    {
        statusEffect = _statusEffect;
    }
}
