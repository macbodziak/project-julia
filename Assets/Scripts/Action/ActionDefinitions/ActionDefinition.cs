using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Base Action Data", menuName = "Scriptable Objects/Actions/Base Action Data Config", order = 300)]
public abstract class ActionDefinition : ScriptableObject
{
    [SerializeField][RequiredField] private string m_name;
    [SerializeField][RangeInt(0, 5)] private int m_actionPointCost = 1;
    [SerializeField][RangeInt(0, 5)] private int m_powerPointCost = 0;
    [SerializeField][RangeInt(0, 10)] private int m_cooldown = 0;
    [SerializeField][RequiredField] private string m_animationTrigger;
    [SerializeField][RequiredField] private Sprite m_sprite;
    [SerializeField] private TargetingMode m_targetingMode;
    [Tooltip("Only used for Multiple Targets, not for Single or All")]
    [SerializeField]
    [RangeInt(1, 5)]
    private int m_numberOfTargets;
    [SerializeField] private VisualEffect m_visualEffectOnHit;
    [SerializeField] private AudioClip m_soundEffect;

    public string Name { get => m_name; protected set => m_name = value; }
    public int ActionPointCost { get => m_actionPointCost; protected set => m_actionPointCost = value; }
    public string AnimationTrigger { get => m_animationTrigger; protected set => m_animationTrigger = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
    public TargetingMode TargetingMode { get { return m_targetingMode; } }
    public int NumberOfTargets { get => m_numberOfTargets; protected set => m_numberOfTargets = value; }
    public VisualEffect VisualEffectOnHitPrefab { get => m_visualEffectOnHit; protected set => m_visualEffectOnHit = value; }
    public int Cooldown { get => m_cooldown; protected set => m_cooldown = value; }
    public int PowerPointCost { get => m_powerPointCost; protected set => m_powerPointCost = value; }
    public AudioClip SoundEffect { get => m_soundEffect; protected set => m_soundEffect = value; }

    public abstract void ExecuteLogic(Unit actingUnit, List<Unit> targets);

    protected void ApplyStatusEffects(Unit target, List<StatusEffectDurationInfo> StatusEffectsApplied)
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
            if (vfxInstance != null)
            {
                Destroy(vfxInstance.gameObject, 5.0f);
            }
            return vfxInstance;
        }
        return null;
    }

}
