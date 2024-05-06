
using UnityEngine;


[CreateAssetMenu(fileName = "Bleeding Status Effect Data", menuName = "Scriptable Objects/Status Effects/Bleeding Status Effect Data Config", order = 10)]
public class BleedingStatusEffectData : BaseStatusEffectData
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private DamageType _damageType = DamageType.Physical;
    // [SerializeField] private ParticleSystem m_particleSystem;

    public int DamageAmount { get => damageAmount; private set => damageAmount = value; }
    public DamageType Type { get => _damageType; private set => _damageType = value; }
    // public ParticleSystem ParticleSystemPrefab { get => m_particleSystem; set => m_particleSystem = value; }
}