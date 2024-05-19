
using System;
using UnityEngine;

[Serializable]
public class StatusEffectDurationInfo
{
    [SerializeField] public StatusEffect statusEffect;
    [SerializeField] public int duration;

    StatusEffectDurationInfo(StatusEffect statusEffectArg, int durationArg)
    {
        statusEffect = statusEffectArg;
        duration = durationArg;
    }
}