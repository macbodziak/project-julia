using UnityEngine;

public class DebuffSingleTargetAction : BaseAction
{
    [SerializeField] BaseActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.SingleEnemyTarget;
    }

    protected override void ExecuteLogic()
    {
        targets[0].ReceiveStatusEffect<SlowStatusEffect>();
    }
}