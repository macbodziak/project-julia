using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

// <summary>
// This class is the component part of the Status Effect
// as a MonoBehaviour it is attached to the gameobject to apply the status effect
// the data and logic of a status effect are provided by the StatusEffect ScriptableObject 
// that needs to be passed in the Initialize method at the beginning of the lifecycle of this component
// </summary>
public class StatusEffectBehaviour : MonoBehaviour
{
    [SerializeField] private StatusEffect _statusEffect;
    [SerializeField][ReadOnly] private int remainingDuration;
    private Unit unit;

    public int Duration { get { return _statusEffect.Duration; } }

    public int RemainingDuration { get => remainingDuration; protected set => remainingDuration = value; }

    public string Name { get { return _statusEffect.Name; } }

    public Sprite Icon { get { return _statusEffect.Icon; } }

    public StatusEffect statusEffect { get { return _statusEffect; } }

    public StatusEffectType statusEffectType { get { return _statusEffect.Type; } }

    public void Initialize(StatusEffect preset)
    {
        _statusEffect = StatusEffect.Instantiate(preset);
    }

    public virtual void ApplyEffect()
    {
        _statusEffect.ApplyEffect();
    }

    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
        remainingDuration = Duration;
        _statusEffect.OnStart(unit);
    }

    public bool IsActive() { return _statusEffect.IsActive; }

    private void OnDestroy()
    {
        _statusEffect.OnEnd();
        Destroy(_statusEffect);
    }
    public void ResetDuration()
    {
        remainingDuration = Duration;
    }

    public void Decrement()
    {
        remainingDuration--;
    }

}
