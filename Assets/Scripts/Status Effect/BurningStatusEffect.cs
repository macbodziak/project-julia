using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningStatusEffect : StatusEffect
{
    [SerializeField] BurningStatusEffectData data;
    [SerializeField] protected ParticleSystem _particleSystem;

    protected void Awake()
    {
        data = LoadData<BurningStatusEffectData>("StatusEffects/Burning Status Effect Data");
    }

    protected override void Start()
    {
        base.Start();
        StartParticleSystem();
    }

    public override void ApplyEffect(Action onCompletedcallback)
    {
        Debug.Log($"applying status effect: {data.Name} with {data.DamageAmount} damage");
        unit.TakeDamage(data.DamageAmount, false);
    }

    public override bool IsAppliedEachTurn() { return true; }

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
