using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{
    [TaskCategory("My Tasks")]
    [TaskDescription("Return Success if the Encounter turn number is equal or greater than threshold")]
    public class TurnNumberEqualOrGreater : Conditional
    {
        [SerializeField] int turnThreshold;

        public override TaskStatus OnUpdate()
        {
            if (TurnManager.Instance.TurnNumber >= turnThreshold)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}