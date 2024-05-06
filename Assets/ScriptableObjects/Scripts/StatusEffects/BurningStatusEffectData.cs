using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Burning Status Effect Data", menuName = "Scriptable Objects/Status Effects/Burning Status Effect Data Config", order = 10)]
public class BurningStatusEffectData : BaseStatusEffectData
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private DamageType _damageType = DamageType.Fire;

    public int DamageAmount { get => damageAmount; private set => damageAmount = value; }
    public ParticleSystem ParticleSystemPrefab { get => m_particleSystem; private set => m_particleSystem = value; }
    public DamageType Type { get => _damageType; private set => _damageType = value; }
}