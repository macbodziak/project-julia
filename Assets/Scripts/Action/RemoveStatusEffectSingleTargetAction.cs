using UnityEngine;

public class RemoveStatusEffectSingleTargetAction : BaseAction
{
    [SerializeField] StatusEffectActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteLogic()
    {
        targets[0].RemoveStatusEffect(data.StatusEffectType);
    }
}