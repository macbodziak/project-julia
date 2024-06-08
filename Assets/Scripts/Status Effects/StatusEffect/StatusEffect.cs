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
    [SerializeField][PreviewTexture] private Sprite m_sprite;
    [Tooltip("will this status effect be executed each turn?")]
    [SerializeField] private ParticleSystem m_vfxPrefab;
    [Tooltip("does this status effect need to executed before regular status effects?")]
    [SerializeField] private bool executeEarly = false;
    [SerializeField] private SavingThrowType m_savingThrowType;
    private ParticleSystem vfxInstance;

    private Unit m_unit;
    public string Name { get => m_name; protected set => m_name = value; }
    public Sprite Icon { get => m_sprite; protected set => m_sprite = value; }
    public abstract bool IsActive { get; }
    public Unit unit { get => m_unit; set => m_unit = value; }
    public abstract StatusEffectType Type { get; }
    public ParticleSystem VisualEffectPrefab { get => m_vfxPrefab; private set => m_vfxPrefab = value; }
    public bool ExecuteEarly { get => executeEarly; protected set => executeEarly = value; }
    public SavingThrowType savingThrowType { get => m_savingThrowType; protected set => m_savingThrowType = value; }


    public virtual void OnStart(Unit effectedUnit)
    {
        unit = effectedUnit;
        StartVFX();
    }


    public virtual void OnEnd()
    {
        StopVFX();
    }


    public virtual void ApplyEffect() {; }


    protected void StartVFX()
    {
        if (VisualEffectPrefab == null)
        {
            return;
        }

        vfxInstance = Instantiate<ParticleSystem>(VisualEffectPrefab, unit.gameObject.transform);

        if (vfxInstance != null)
        {
            vfxInstance.transform.parent = unit.gameObject.transform;
            // vfxInstance.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    protected void StopVFX()
    {
        vfxInstance.Stop();
        Animator animator = vfxInstance.gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
        }
        Destroy(vfxInstance.gameObject, 0.5f);
    }

}
