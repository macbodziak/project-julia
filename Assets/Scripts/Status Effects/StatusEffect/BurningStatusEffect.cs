using UnityEngine;

[CreateAssetMenu(fileName = "Burning Status Effect", menuName = "Scriptable Objects/Status Effects/Burning Status Effect Preset", order = 10)]
public class BurningStatusEffect : StatusEffect
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private DamageType m_damageType = DamageType.Fire;
    private ParticleSystem particleSystemInstance;

    public int DamageAmount { get => damageAmount; private set => damageAmount = value; }
    public DamageType damageType { get => m_damageType; private set => m_damageType = value; }
    public override bool IsActive { get { return true; } }
    public override StatusEffectType Type { get { return StatusEffectType.Burning; } }

    public override void OnEnd()
    {
        particleSystemInstance.Stop();
        Destroy(particleSystemInstance.gameObject, 1f);
    }

    public override void OnStart(Unit effectedUnit)
    {
        base.OnStart(effectedUnit);
        if (ParticleSystemPrefab != null)
        {
            StartParticleSystem();
        }
    }

    public override void ApplyEffect()
    {
        unit.combatStats.TakeDamage(DamageAmount, damageType, false, false);
    }

    private void StartParticleSystem()
    {
        particleSystemInstance = Instantiate<ParticleSystem>(ParticleSystemPrefab);

        if (particleSystemInstance != null)
        {
            particleSystemInstance.transform.parent = unit.gameObject.transform;
            particleSystemInstance.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}