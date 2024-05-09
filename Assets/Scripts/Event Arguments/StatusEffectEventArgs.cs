using System;

public class StatusEffectEventArgs : EventArgs
{
    public StatusEffectBehaviour statusEffect;
    public StatusEffectEventArgs(StatusEffectBehaviour _statusEffect)
    {
        statusEffect = _statusEffect;
    }
}
