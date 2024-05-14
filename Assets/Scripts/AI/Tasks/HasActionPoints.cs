using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasActionPoints : Conditional
{
	public override TaskStatus OnUpdate()
	{
		Unit unit = GetComponent<Unit>();
		Debug.Log("Unit" + unit.gameObject + " has " + unit.ActionPoints + " action points");
		return TaskStatus.Success;
	}
}