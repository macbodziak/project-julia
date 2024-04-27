using UnityEngine;

[CreateAssetMenu(fileName = "Multiple Attack Config Data", menuName = "Scriptable Objects/Multiple Attack Config Data", order = 2)]
public class MultipleAttackActionData : AttackActionData
{
    [SerializeField] private int numberOfTargets;

    public int NumberOfTargets { get => numberOfTargets; protected set => numberOfTargets = value; }
}
