using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Base Status Effect Data", menuName = "Scriptable Objects/Status Effects/Base Status Effect Data Config", order = 10)]
public abstract class StatusEffect : ScriptableObject
{
    [SerializeField] private string m_name = "no name";
    [SerializeField] private Sprite m_sprite;
    [Tooltip("will this status effect be executed each turn?")]
    [SerializeField] private VisualEffect m_visualEffect;
    [Tooltip("does this status effect need to executed before regular status effects?")]
    [SerializeField] private bool executeEarly = false;

    private Unit m_unit;
    public string Name { get => m_name; protected set => m_name = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
    public abstract bool IsActive { get; }
    public Unit unit { get => m_unit; set => m_unit = value; }
    public abstract StatusEffectType Type { get; }
    public VisualEffect VisualEffectPrefab { get => m_visualEffect; private set => m_visualEffect = value; }
    public bool ExecuteEarly { get => executeEarly; protected set => executeEarly = value; }

    public virtual void OnStart(Unit effectedUnit)
    {
        unit = effectedUnit;
    }

    public abstract void OnEnd();

    public virtual void ApplyEffect() {; }

}
