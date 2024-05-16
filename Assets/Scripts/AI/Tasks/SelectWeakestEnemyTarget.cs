using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects the Target with the lowest HP\nSelects only one Target")]

    public class SelectWeakestEnemyTarget : Action
    {
        [SerializeField] SharedUnitList selectedTargets;


        public override TaskStatus OnUpdate()
        {
            List<Unit> units = CombatEncounterManager.Instance.GetPlayerUnitList();

            if (units.Count == 0)
            {
                return TaskStatus.Failure;
            }

            Unit potentialTarget = units[0];
            foreach (Unit unit in units)
            {
                if (unit.combatStats.CurrentHealthPoints < potentialTarget.combatStats.CurrentHealthPoints)
                {
                    potentialTarget = unit;
                }
            }

            selectedTargets.Value = new();
            selectedTargets.Value.Add(potentialTarget);
            return TaskStatus.Success;
        }
    }
}