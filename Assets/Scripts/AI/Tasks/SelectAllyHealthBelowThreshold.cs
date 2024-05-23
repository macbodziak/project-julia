using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace EnemyAI
{
	[TaskCategory("My Tasks/Target Selection")]
	[TaskDescription("Selects random ally target that has health below given threshold or returns Failure if no ally meets criteria\nThreshold should be between 0 and 1\nSelects only one target")]
	public class SelectAllyHealthBelowThreshold : Action
	{
		[SerializeField] SharedUnitList selectedTargets;
		[SerializeField] float threshold;


		public override TaskStatus OnUpdate()
		{
			List<Unit> unitList = GameManagement.EncounterManager.Instance.GetEnemyUnitList();
			List<Unit> potentialTargets = new();
			float currentRatio;

			foreach (Unit unit in unitList)
			{
				currentRatio = (float)unit.combatStats.CurrentHealthPoints / (float)unit.combatStats.MaxHealthPoints;
				if (currentRatio < threshold)
				{
					potentialTargets.Add(unit);
				}
			}

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