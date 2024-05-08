using UnityEngine;

public class DebuffSingleTargetAction : BaseAction
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
        targets[0].TryReceivingStatusEffect(data.StatusEffectType);
    }
}