using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    [SerializeField] protected BaseStatusEffectData baseData;
    private int remainingDuration;
    protected Unit unit;

    public int Duration
    {
        get { return baseData.Duration; }

    }

    public int RemainingDuration { get => remainingDuration; protected set => remainingDuration = value; }
    public string Name
    {
        get { return baseData.Name; }
    }

    public Sprite Icon
    {
        get { return baseData.Icon; }
    }

    // public List<int> Stack { get => stack; protected set => stack = value; }

    public abstract void ApplyEffect(Action onCompletedcallback);

    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
    }

    public virtual void OnEffectReceived() {; }

    public virtual void OnEffectExpired() {; }

    public virtual bool IsAppliedEachTurn() { return false; }

    public void ResetDuration()
    {
        remainingDuration = Duration;
    }

    public void Decrement()
    {
        remainingDuration--;
    }
}
