using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace EnemyAI
{
	[TaskCategory("My Tasks")]
	[TaskDescription("Chooses a Random action that the unit can perform. Returns Failure if no actions are available,"
	+ " for example not enough action points or still on cooldown. Sets the value of \"Selected Action\"")]
	public class ChooseRandomAction : Action
	{
		private Unit myUnit;
		private ActionController actionController;
		private List<ActionBehaviour> availableActions;
		public SharedActionBehaviour SelectedAction;
		public override void OnAwake()
		{
			myUnit = GetComponent<Unit>();
			actionController = GetComponent<ActionController>();

		}

		public override TaskStatus OnUpdate()
		{
			availableActions = actionController.GetAvailableActions();
			if (availableActions.Count == 0)
			{
				return TaskStatus.Failure;
			}

			//DEBUG
			DebugLogAvailabeActions();

			int randomIndex = UnityEngine.Random.Range(0, availableActions.Count);
			ActionBehaviour choosenAction = availableActions[randomIndex];
			Debug.Log("Chosen Action: [" + randomIndex + "] : " + choosenAction.actionDefinition);

			SelectedAction.Value = choosenAction;
			return TaskStatus.Success;
		}

		private void DebugLogAvailabeActions()
		{
			Debug.Log("<color=#ffa8a8>available Actions:</color>");
			foreach (var action in availableActions)
			{
				Debug.Log("<color=#ffa8a8>--- " + action.actionDefinition.name + "</color>");
			}
		}
	}
}