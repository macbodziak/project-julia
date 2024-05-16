using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{
	[TaskCategory("My Tasks")]
	[TaskDescription("Starts the \"Selected Action\" with the \"Selected Targets\". Both Shared Variables need to be set (excpet for no target Actions)")]
	public class PerformSelectedAction : Action
	{
		bool isRunning = false;
		bool hasStarted = false;
		[SerializeField] SharedActionBehaviour selectedAction;
		[SerializeField] SharedUnitList selectedTargets;
		public override void OnStart()
		{
			isRunning = false;
			hasStarted = false;
		}

		public override TaskStatus OnUpdate()
		{
			if (hasStarted == false)
			{
				hasStarted = true;
				isRunning = true;

				if (selectedAction.Value == null)
				{
					return TaskStatus.Failure;
				}

				if (selectedAction.Value.targetingMode != TargetingMode.NoTarget && selectedTargets.Value == null)
				{
					return TaskStatus.Failure;
				}
				Debug.Log("the following action has been selected for execution: <color=#ffe05e>" + selectedAction.Value.actionDefinition + "</color>");
				selectedAction.Value.StartAction(selectedTargets.Value, OnActionCompleted);
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
}