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
            for (int i = 1; i < units.Count; i++)
            {
                if (units[i].combatStats.CurrentHealthPoints < potentialTarget.combatStats.CurrentHealthPoints)
                {
                    potentialTarget = units[i];
                }
                else if (units[i].combatStats.CurrentHealthPoints == potentialTarget.combatStats.CurrentHealthPoints)
                {
                    if (Random.value < 0.5f)
                    {
                        potentialTarget = units[i];
                    }
                }
            }

            selectedTargets.Value = new();
            selectedTargets.Value.Add(potentialTarget);
            return TaskStatus.Success;
        }
    }
}