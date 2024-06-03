using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    namespace Demon
    {

        [TaskCategory("My Tasks/Demon/Target Selection")]
        [TaskDescription("Select Target for Heavy Attack")]

        public class SelectHeavyAttackTarget : Action
        {
            [SerializeField] SharedUnitList selectedTargets;


            public override TaskStatus OnUpdate()
            {
                List<Unit> units = GameManagement.EncounterManager.Instance.GetPlayerUnitList();
                List<int> scores = new List<int>(units.Count);

                if (units.Count == 0)
                {
                    return TaskStatus.Failure;
                }

                for (int i = 0; i < units.Count; i++)
                {
                    scores.Add(0);

                    if (units[i].statusEffectController.HasStatusEffect(StatusEffectType.Unsteady) == false)
                    {
                        scores[i] += 100;
                    }

                    if (units[i].statusEffectController.HasStatusEffect(StatusEffectType.DemonsMark))
                    {
                        scores[i] += 30;
                    }

                    if (units[i].combatStats.CurrentHealthPoints < 50)
                    {
                        scores[i] += 50 - units[i].combatStats.CurrentHealthPoints;
                    }

                    scores[i] -= units[i].combatStats.GetTotalDamageResistance(DamageType.Fire);

                }

                selectedTargets.Value = new();
                selectedTargets.Value.Add(Utilities.SelectTargetByScore(units, scores));
                return TaskStatus.Success;
            }
        }
    }
}