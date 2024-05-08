using UnityEngine;


[CreateAssetMenu(fileName = "Rage Status Effect Data", menuName = "Scriptable Objects/Status Effects/Rage Status Effect Data Config", order = 10)]
public class RageStatusEffectData : BaseStatusEffectData
{
    [SerializeField] private float damageMultiplier = 1;
    [SerializeField] private int hitChanceModifier = 1;
    [SerializeField] private int critChanceModifier = 1;

    public float DamageMultiplier { get => damageMultiplier; protected set => damageMultiplier = value; }
    public int HitChanceModifier { get => hitChanceModifier; protected set => hitChanceModifier = value; }
    public int CritChanceModifier { get => critChanceModifier; protected set => critChanceModifier = value; }
}