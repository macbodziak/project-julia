using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Burning Status Effect", menuName = "Scriptable Objects/Status Effects/Burning Status Effect Preset", order = 10)]
public class BurningStatusEffect : StatusEffect
{
    [SerializeField] private int minDamageAmount = 1;
    [SerializeField] private int maxDamageAmount = 2;
    [SerializeField] private DamageType m_damageType = DamageType.Fire;
    private VisualEffect visualEffectInstance;

    public int MinDamageAmount { get => minDamageAmount; private set => minDamageAmount = value; }
    public int MaxDamageAmount { get => maxDamageAmount; private set => maxDamageAmount = value; }
    public DamageType damageType { get => m_damageType; private set => m_damageType = value; }
    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.Burning; } }

    public override void OnEnd()
    {
        visualEffectInstance.Stop();
        Animator animator = visualEffectInstance.gameObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
        }
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
        int damageAmount = UnityEngine.Random.Range(minDamageAmount, maxDamageAmount);
        unit.combatStats.TakeDamage(damageAmount, damageType, false, false);
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