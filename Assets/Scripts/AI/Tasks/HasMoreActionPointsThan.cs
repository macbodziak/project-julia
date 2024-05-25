using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{
    [TaskCategory("My Tasks")]
    [TaskDescription("Return Success if the unit has at least \"amount\" Action Points left")]
    public class HasMoreActionPointsThan : Conditional
    {
        [SerializeField] private int amount;

        public override TaskStatus OnUpdate()
        {
            Unit unit = GetComponent<Unit>();
            Debug.Log("Unit" + unit.gameObject + " has " + unit.ActionPoints + " action points");
            if (unit.ActionPoints > amount)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}