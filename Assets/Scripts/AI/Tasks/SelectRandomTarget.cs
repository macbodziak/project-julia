using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SelectRandomTarget : Action
{
	[SerializeField] SharedActionBehaviour selectedAction;

	public override void OnStart()
	{

	}

	public override TaskStatus OnUpdate()
	{
		TargetingMode targetingMode = selectedAction.Value.actionDefinition.TargetingMode;


		return TaskStatus.Success;
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
				selectedTargets = new();
				selectedTargets.Add(GetComponent<Unit>());
				break;
			case TargetingMode.SingleAllyTarget:
				// GetSingleAllyTarget();
				break;
			case TargetingMode.AllAllyTargets:
				// GetAllAllyTargets();
				break;
			case TargetingMode.NoTarget:
				break;
		}

		return selectedTargets;

	}


	private List<Unit> GetRandomSingleEnemyTarget()
	{
		List<Unit> selectedUnits = new();
		List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
		int count = playerUnits.Count;
		selectedUnits.Add(playerUnits[UnityEngine.Random.Range(0, count)]);
		return selectedUnits;
	}


	private List<Unit> GetMultipleEnemyTargets(int numberOfTargets)
	{
		List<Unit> playerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
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
		return CombatEncounterManager.Instance.GetPlayerUnitList();
	}

}