using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Burning Status Effect", menuName = "Scriptable Objects/Status Effects/Burning Status Effect Preset", order = 10)]
public class BurningStatusEffect : StatusEffect
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private DamageType m_damageType = DamageType.Fire;
    private VisualEffect visualEffectInstance;

    public int DamageAmount { get => damageAmount; private set => damageAmount = value; }
    public DamageType damageType { get => m_damageType; private set => m_damageType = value; }
    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.Burning; } }

    public override void OnEnd()
    {
        visualEffectInstance.Stop();
        Destroy(visualEffectInstance.gameObject, 1f);
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        if (VisualEffectPrefab != null)
        {
            StartVisualEffect();
        }
    }

    public override void ApplyEffect()
    {
        unit.combatStats.TakeDamage(DamageAmount, damageType, false, false);
    }

    private void StartVisualEffect()
    {
        visualEffectInstance = Instantiate<VisualEffect>(VisualEffectPrefab);

        if (visualEffectInstance != null)
        {
            visualEffectInstance.transform.parent = unit.gameObject.transform;
            visualEffectInstance.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}