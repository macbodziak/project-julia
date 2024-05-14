using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class ChooseRandomAction : Action
{
	private Unit myUnit;
	private ActionController actionController;
	private List<ActionBehaviour> availableActions;
	public SharedActionBehaviour SelectedAction;
	public override void OnStart()
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
		Debug.Log("<color=#ffa8a8>available Actions:</color>");
		foreach (var action in availableActions)
		{
			Debug.Log("<color=#ffa8a8>--- " + action.actionDefinition.name + "</color>");
		}

		// /Debug
		int randomIndex = UnityEngine.Random.Range(0, availableActions.Count);
		ActionBehaviour choosenAction = availableActions[randomIndex];
		Debug.Log("Chosen Action: [" + randomIndex + "] : " + choosenAction.actionDefinition);

		SelectedAction.Value = choosenAction;
		return TaskStatus.Success;
	}
}