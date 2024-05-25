using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{

    [TaskCategory("My Tasks")]
    [TaskDescription("Returns Success if the HP of the unit is below provied value.")]
    public class HealthBelowAbsoluteValue : Conditional
    {
        [SerializeField] float value;
        Unit unit;


        public override void OnAwake()
        {
            unit = GetComponent<Unit>();
        }


        public override TaskStatus OnUpdate()
        {
            if (unit.combatStats.CurrentHealthPoints < value)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}