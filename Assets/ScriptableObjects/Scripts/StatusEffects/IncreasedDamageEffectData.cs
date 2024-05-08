using UnityEngine;

[CreateAssetMenu(fileName = "Increase Damage Status Effect Data", menuName = "Scriptable Objects/Status Effects/Increase Damage Status Effect Data Config", order = 3)]
public class IncreasedDamageEffectData : BaseStatusEffectData
{
    [SerializeField] private float damageMultiplier = 1.1f;

    public float DamageMultiplier { get => damageMultiplier; private set => damageMultiplier = value; }
}