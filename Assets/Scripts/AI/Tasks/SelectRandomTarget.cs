using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace EnemyAI
{
	[TaskCategory("My Tasks/Target Selection")]
	[TaskDescription("Selects Random Target(s) based on the Targeting Mode of the \"Selected Action\" (Shared Variable) and sets the value of \"Selected Targets\"")]
	public class SelectRandomTarget : Action
	{
		[SerializeField] SharedActionBehaviour selectedAction;
		[SerializeField] SharedUnitList selectedTargets;
		bool taskFailed = false;

		public override void OnStart()
		{
			taskFailed = false;
		}

		public override TaskStatus OnUpdate()
		{
			TargetingMode targetingMode = selectedAction.Value.actionDefinition.TargetingMode;
			selectedTargets.Value = GetRandomTargets(targetingMode);
			if (taskFailed)
			{
				return TaskStatus.Failure;
			}
			else
			{
				DebugLogSelectedTargets();
				return TaskStatus.Success;
			}
		}

		private List<Unit> GetRandomTargets(TargetingMode targetingMode)
		{
			List<Unit> selectedTargets = null;
			switch (targetingMode)
			{
				case TargetingMode.SingleEnemyTarget:
					selectedTargets = GetRandomSingleEnemyTarget();
					break;
				case TargetingMode.MultipleEnemyTargets:
					selectedTargets = GetMultipleEnemyTargets(selectedAction.Value.actionDefinition.NumberOfTargets);
					break;
				case TargetingMode.AllEnemyTargets:
					selectedTargets = GetAllEnemyTargets();
					break;
				case TargetingMode.SelfTarget:
					selectedTargets = GetSelfTarget();
					break;
				case TargetingMode.SingleAllyTarget:
					selectedTargets = GetSingleAllyTarget();
					break;
				case TargetingMode.AllAllyTargets:
					selectedTargets = GetAllAllyTargets();
					break;
				case TargetingMode.NoTarget:
					break;
			}

			return selectedTargets;

		}


		private List<Unit> GetAllAllyTargets()
		{
			return CombatEncounterManager.Instance.GetEnemyUnitList();
		}


		private List<Unit> GetSingleAllyTarget()
		{
			List<Unit> selectedUnits = new();
			List<Unit> enemyUnits = CombatEncounterManager.Instance.GetEnemyUnitList();
			int count = enemyUnits.Count;

			//check if there are still enemy characters alive, or the previous action in this turn has killed all
			if (count > 0)
			{
				selectedUnits.Add(enemyUnits[UnityEngine.Random.Range(0, count)]);
				return selectedUnits;
			}
			taskFailed = true;
			return null;
		}


		private List<Unit> GetSelfTarget()
		{
			List<Unit> selectedTargets = new();
			selectedTargets.Add(GetComponent<Unit>());
			return selectedTargets;
		}


		private List<Unit> GetRandomSingleEnemyTarget()
		{
			List<Unit> selectedUnits = new();
			List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
			int count = playerUnits.Count;

			//check if there are still player characters, or the previous action in this turn has killed all
			if (count > 0)
			{
				selectedUnits.Add(playerUnits[UnityEngine.Random.Range(0, count)]);
				return selectedUnits;
			}
			else
			{
				taskFailed = true;
				return null;
			}
		}


		private List<Unit> GetMultipleEnemyTargets(int numberOfTargets)
		{
			List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();

			//check if there are still player characters, or the previous action in this turn has killed all
			if (playerUnits.Count == 0)
			{
				taskFailed = true;
				return null;
			}

			List<Unit> playerUnitsCopy = new List<Unit>(playerUnits);
			List<Unit> selectedUnits = new();
			if (numberOfTargets > playerUnits.Count)
			{
				numberOfTargets = playerUnits.Count;
			}
			for (int i = 0; i < numberOfTargets; i++)
			{
				int randomIndex = UnityEngine.Random.Range(0, playerUnitsCopy.Count);
				selectedUnits.Add(playerUnitsCopy[randomIndex]);
				playerUnitsCopy.RemoveAt(randomIndex);
			}

			return selectedUnits;
		}


		private List<Unit> GetAllEnemyTargets()
		{
			if (CombatEncounterManager.Instance.GetPlayerUnitList().Count > 0)
			{
				return CombatEncounterManager.Instance.GetPlayerUnitList();
			}
			//the if statement above failes if there are no more player units alive
			taskFailed = true;
			return null;
		}


		private void DebugLogSelectedTargets()
		{
			Debug.Log("<color=#5ec8ff>Selected Targets:</color>");
			foreach (var target in selectedTargets.Value)
			{
				Debug.Log("<color=#5ec8ff>--- " + target.gameObject + "</color>");
			}
		}

	}
}