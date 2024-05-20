using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{
    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects Self as Selected Target if does NOT have given Status Effect\n")]
    public class SelectSelfIfNoStatusEffect : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        [SerializeField] StatusEffectType type;
        StatusEffectController statusEffectController;
        Unit unit;


        public override void OnAwake()
        {
            statusEffectController = GetComponent<StatusEffectController>();
            unit = GetComponent<Unit>();
        }


        public override TaskStatus OnUpdate()
        {
            if (statusEffectController.HasStatusEffect(type) == false)
            {
                selectedTargets.Value = new();
                selectedTargets.Value.Add(unit);
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}