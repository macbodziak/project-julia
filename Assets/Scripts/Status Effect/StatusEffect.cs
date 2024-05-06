using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

// <summary>
// This is the base class for status effects
// To create a specific status effect:
// 1. create a Scritpalbe Object to hold data sepcific to this effect 
//    derived from BaseStatusEffectData
// 2. create a class derived from this class and implemet its ApplyEffect method
//    and add effect specific data and behaviour
// 3. override the IsAppliedEachTurn() method to return true, if the effect should
//    be apllied each turn, like do damage each turn
// 4. call the LoadData method in the derived Awake method with the path to the 
//    Scriptable Object containing data for the specific status effect
// </summary>
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

    public abstract void ApplyEffect();

    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
    }


    public virtual bool IsAppliedEachTurn() { return false; }

    public void ResetDuration()
    {
        remainingDuration = Duration;
    }

    public void Decrement()
    {
        remainingDuration--;
    }

    protected T LoadData<T>(string path) where T : BaseStatusEffectData
    {
        T data = Resources.Load<T>(path);
        if (data == null)
        {
            Debug.LogWarning("not able to load data at path: " + path, this);
        }
        RemainingDuration = data.Duration;
        return data;
    }
}