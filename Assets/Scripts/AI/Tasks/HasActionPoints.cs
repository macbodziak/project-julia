using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{
	[TaskCategory("My Tasks")]
	[TaskDescription("Return Success if the unit has Action Points left")]
	public class HasActionPoints : Conditional
	{
		public override TaskStatus OnUpdate()
		{
			Unit unit = GetComponent<Unit>();
			Debug.Log("Unit" + unit.gameObject + " has " + unit.ActionPoints + " action points");
			if (unit.ActionPoints > 0)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}