using UnityEngine;

// <summary
// apply fire damage over several rounds
// </summary>
public class BurningStatusEffect : StatusEffect
{
    [SerializeField] BurningStatusEffectData data;
    [SerializeField] protected ParticleSystem _particleSystem;

    protected void Awake()
    {
        data = LoadData<BurningStatusEffectData>("StatusEffects/Burning Status Effect Data");
        baseData = data;
    }

    protected override void Start()
    {
        base.Start();
        StartParticleSystem();
    }

    public override void ApplyEffect()
    {
        unit.combatStats.TakeDamage(data.DamageAmount, data.Type, false, false);
    }

    public override bool IsActive() { return true; }

    protected void OnDestroy()
    {
        _particleSystem.Stop();
        Destroy(_particleSystem.gameObject, 1f);
    }

    private void StartParticleSystem()
    {
        _particleSystem = Instantiate<ParticleSystem>(data.ParticleSystemPrefab);

        if (_particleSystem != null)
        {
            _particleSystem.transform.parent = gameObject.transform;
            _particleSystem.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}
