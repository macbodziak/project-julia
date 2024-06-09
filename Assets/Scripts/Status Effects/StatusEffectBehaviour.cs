using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// This class is the component part of the Status Effect
// as a MonoBehaviour it is attached to the gameobject to apply the status effect
// the data and logic of a status effect are provided by the StatusEffect ScriptableObject 
// that needs to be passed in the Initialize method at the beginning of the lifecycle of this component
// </summary>

[Serializable]
public class StatusEffectBehaviour : MonoBehaviour
{
    [SerializeField] private StatusEffect _statusEffect;
    [SerializeField] private int remainingDuration;
    private Unit unit;

    public int RemainingDuration { get => remainingDuration; protected set => remainingDuration = value; }

    public string Name { get { return _statusEffect.Name; } }

    public Sprite Icon { get { return _statusEffect.Icon; } }

    public StatusEffect statusEffect { get { return _statusEffect; } }

    public StatusEffectType statusEffectType { get { return _statusEffect.Type; } }

    public void Initialize(StatusEffect preset, int duration)
    {
        _statusEffect = StatusEffect.Instantiate(preset);
        remainingDuration = duration;
    }


    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
        _statusEffect.OnStart(unit);
    }

    public bool IsAppliedEachTurn()
    {
        if (_statusEffect is IAppliedEachTurn)
        {
            return true;
        }
        return false;
    }

    private void OnDestroy()
    {
        _statusEffect.OnEnd();
        Destroy(_statusEffect);
    }
    public void ResetDuration(int duration)
    {
        remainingDuration = duration;
    }

    public void Decrement()
    {
        remainingDuration--;
    }

}
