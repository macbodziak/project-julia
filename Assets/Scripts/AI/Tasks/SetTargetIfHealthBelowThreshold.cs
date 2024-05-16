using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("If HP percantage of the unit is below provided threshold set this unit as the Selected Target and return Success.\nThreshold should be between 0 and 1")]
    [TaskIcon("Assets/Sprites/Behaviour Designer Icons/Health_Percentage.png")]
    [TaskName("Set as Target if HP < %")]
    public class SetTargetIfHealthBelowThreshold : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        [SerializeField] float threshold;
        Unit unit;


        public override void OnAwake()
        {
            unit = GetComponent<Unit>();
        }


        public override TaskStatus OnUpdate()
        {
            float currentRatio = (float)unit.combatStats.CurrentHealthPoints / (float)unit.combatStats.MaxHealthPoints;

            if (currentRatio < threshold)
            {
                List<Unit> target = new List<Unit> { unit };
                selectedTargets.Value = target;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}