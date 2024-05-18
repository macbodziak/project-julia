using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using EnemyAI;
using System.Collections.Generic;


namespace EnemyAI
{
    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Selects random Target with given Status Effect\nSelects only one Target")]
    public class SelectAllyWithoutStatusEffect : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        [SerializeField] StatusEffectType type;


        public override TaskStatus OnUpdate()
        {
            List<Unit> units = CombatEncounterManager.Instance.GetEnemyUnitList();

            if (units.Count == 0)
            {
                return TaskStatus.Failure;
            }

            //create a list of all targets without given status effect
            List<Unit> potentialTargets = new();
            foreach (Unit unit in units)
            {
                if (unit.statusEffectController.HasStatusEffect(type) == true)
                {
                    potentialTargets.Add(unit);
                }
            }

            //of no potential target
            if (potentialTargets.Count == 0)
            {
                return TaskStatus.Failure;
            }


            int randomIndex = UnityEngine.Random.Range(0, potentialTargets.Count);

            selectedTargets.Value = new();
            selectedTargets.Value.Add(potentialTargets[randomIndex]);
            return TaskStatus.Success;
        }

    }
}