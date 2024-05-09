using UnityEngine;

[CreateAssetMenu(fileName = "Status Effect Config Data", menuName = "Scriptable Objects/Actions/Status Effect Action Config Data", order = 3)]
public class StatusEffectActionData : BaseActionData
{
    [SerializeField] private StatusEffect m_statusEffect;

    public StatusEffect statusEffect { get => m_statusEffect; protected set => m_statusEffect = value; }
}
