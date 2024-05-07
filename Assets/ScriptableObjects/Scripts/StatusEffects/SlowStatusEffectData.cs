using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Slow Status Effect Data", menuName = "Scriptable Objects/Status Effects/Slow Status Effect Data Config", order = 10)]
public class SlowStatusEffectData : BaseStatusEffectData
{
    [SerializeField] private int actionPointModifier = 1;

    public int ActionPointModifier { get => actionPointModifier; private set => actionPointModifier = value; }
}