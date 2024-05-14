using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PerformSelectedAction : Action
{
	bool isRunning = false;
	bool hasStarted = false;
	public SharedActionBehaviour selectedAction;
	public override void OnStart()
	{
		isRunning = false;
		hasStarted = false;
		Debug.Log("the following action has been selected for execution: " + selectedAction.Value.actionDefinition);
	}

	public override TaskStatus OnUpdate()
	{
		if (hasStarted == false)
		{
			hasStarted = true;
			isRunning = true;
			selectedAction.Value.StartAction(CombatEncounterManager.Instance.GetPlayerUnitList(), OnActionCompleted);

		}

		if (isRunning)
		{
			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Success;
		}
	}

	private void OnActionCompleted()
	{
		isRunning = false;
	}
}