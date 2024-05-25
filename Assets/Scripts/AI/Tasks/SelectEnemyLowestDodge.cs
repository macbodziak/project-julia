using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects the Target with the lowest Dodge\nSelects only one Target")]

    public class SelectEnemyLowestDodge : Action
    {
        [SerializeField] SharedUnitList selectedTargets;


        public override TaskStatus OnUpdate()
        {
            List<Unit> units = GameManagement.EncounterManager.Instance.GetPlayerUnitList();

            if (units.Count == 0)
            {
                return TaskStatus.Failure;
            }

            Unit potentialTarget = units[0];
            for (int i = 1; i < units.Count; i++)
            {
                if (units[i].combatStats.TotalDodge < potentialTarget.combatStats.TotalDodge)
                {
                    potentialTarget = units[i];
                }
                else if (units[i].combatStats.TotalDodge == potentialTarget.combatStats.TotalDodge)
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