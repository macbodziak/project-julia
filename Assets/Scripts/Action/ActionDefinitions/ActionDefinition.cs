using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Base Action Data", menuName = "Scriptable Objects/Actions/Base Action Data Config", order = 300)]
public abstract class ActionDefinition : ScriptableObject
{
    [SerializeField] private string m_name = "no name";
    [SerializeField] private int m_actionPointCost = 1;
    [SerializeField] private float m_duration = 5f;
    [SerializeField] private string m_animationTrigger;
    [SerializeField] private Sprite m_sprite;
    [SerializeField] private TargetingModeType m_targetingMode;
    [Tooltip("Only used for Multiple Targets, not for Single or All")][SerializeField] private int m_numberOfTargets;
    [SerializeField] private VisualEffect m_visualEffectOnHit;

    public string Name { get => m_name; protected set => m_name = value; }
    public int ActionPointCost { get => m_actionPointCost; protected set => m_actionPointCost = value; }
    public float Duration { get => m_duration; protected set => m_duration = value; }
    public string AnimationTrigger { get => m_animationTrigger; protected set => m_animationTrigger = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
    public TargetingModeType TargetingMode { get { return m_targetingMode; } }
    public int NumberOfTargets { get => m_numberOfTargets; set => m_numberOfTargets = value; }
    public VisualEffect VisualEffectOnHitPrefab { get => m_visualEffectOnHit; set => m_visualEffectOnHit = value; }

    public abstract void ExecuteLogic(Unit actingUnit, List<Unit> targets);

    protected void ApplyStatusEffects(Unit target, List<StatusEffect> StatusEffectsApplied)
    {
        if (StatusEffectsApplied.Count > 0)
        {
            for (int i = 0; i < StatusEffectsApplied.Count; i++)
            {
                target.statusEffectController.TryReceivingStatusEffect(StatusEffectsApplied[i]);
            }
        }
    }

    protected void RemoveStatusEffects(Unit target, List<StatusEffect> StatusEffectsRemoved)
    {
        if (StatusEffectsRemoved.Count > 0)
        {
            for (int i = 0; i < StatusEffectsRemoved.Count; i++)
            {
                target.statusEffectController.RemoveStatusEffect(StatusEffectsRemoved[i]);
            }
        }
    }

    protected VisualEffect PlayVisualEffect(VisualEffect vfx, Transform transformArg)
    {
        if (vfx != null)
        {
            VisualEffect vfxInstance = Instantiate<VisualEffect>(vfx, transformArg);
            return vfxInstance;
        }
        return null;
    }
}