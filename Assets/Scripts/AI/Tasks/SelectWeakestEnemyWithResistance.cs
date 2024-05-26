using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects the Target with the lowest HP\nIgnores Targets that have Damage Resistance above Threshold\n" +
    "Required Selected Action to be set first\nSelects only one Target")]

    public class SelectWeakestEnemyWithResistance : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        [SerializeField] SharedActionBehaviour selectedAction;
        [SerializeField] int damageResistanceThreshold;

        public override TaskStatus OnUpdate()
        {
            List<Unit> units = GameManagement.EncounterManager.Instance.GetPlayerUnitList();

            if (units.Count == 0)
            {
                return TaskStatus.Failure;
            }

            List<Unit> candidateUnits;
            if (damageResistanceThreshold == 0)
            {
                candidateUnits = units;
            }
            else
            {
                candidateUnits = new();
                foreach (Unit unit in units)
                {
                    if (Utilities.UnitHasDamageResistanceBelow(unit, selectedAction, damageResistanceThreshold))
                    {
                        candidateUnits.Add(unit);
                    }
                }
                if (candidateUnits.Count == 0)
                {
                    return TaskStatus.Failure;
                }
            }


            Unit potentialTarget = candidateUnits[0];
            for (int i = 1; i < candidateUnits.Count; i++)
            {
                if (candidateUnits[i].combatStats.CurrentHealthPoints < potentialTarget.combatStats.CurrentHealthPoints)
                {
                    potentialTarget = candidateUnits[i];
                }
                else if (candidateUnits[i].combatStats.CurrentHealthPoints == potentialTarget.combatStats.CurrentHealthPoints)
                {
                    if (Random.value < 0.5f)
                    {
                        potentialTarget = candidateUnits[i];
                    }
                }
            }

            selectedTargets.Value = new();
            selectedTargets.Value.Add(potentialTarget);
            return TaskStatus.Success;
        }
    }
}