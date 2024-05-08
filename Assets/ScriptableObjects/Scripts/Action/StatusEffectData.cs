using UnityEngine;

[CreateAssetMenu(fileName = "Status Effect Config Data", menuName = "Scriptable Objects/Actions/Status Effect Action Config Data", order = 3)]
public class StatusEffectActionData : BaseActionData
{
    [SerializeField] private StatusEffectType statusEffectType;

    public StatusEffectType StatusEffectType { get => statusEffectType; protected set => statusEffectType = value; }
}
