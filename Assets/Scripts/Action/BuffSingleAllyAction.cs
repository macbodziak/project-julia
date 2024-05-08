using UnityEngine;

public class BuffSingleAllyAction : BaseAction
{
    [SerializeField] StatusEffectActionData data;
    protected override void Awake()
    {
        baseData = data;
        base.Awake();
        actionType = ActionType.SingleAllyTarget;
    }

    protected override void ExecuteLogic()
    {
        targets[0].TryReceivingStatusEffect(data.StatusEffectType);
    }
}