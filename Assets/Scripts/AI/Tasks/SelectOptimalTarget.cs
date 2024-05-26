using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects optimal Target based on HP, Dodge and Damage Resistance\nREquires Selected Target to be Set\n"
     + "Selects only one Target")]

    public class SelectOptimalTarget : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        [SerializeField] SharedActionBehaviour selectedAction;


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
                if (Utilities.Score(units[i], selectedAction) < Utilities.Score(potentialTarget, selectedAction))
                {
                    potentialTarget = units[i];
                }
                else if (Utilities.Score(units[i], selectedAction) == Utilities.Score(potentialTarget, selectedAction))
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