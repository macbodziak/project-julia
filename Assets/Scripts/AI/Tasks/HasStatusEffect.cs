using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasStatusEffect : Conditional
{
	[SerializeField] StatusEffectType statusEffectType;
	Unit unit;

	public override void OnAwake()
	{
		unit = GetComponent<Unit>();
	}


	public override TaskStatus OnUpdate()
	{
		if (unit.statusEffectController.HasStatusEffect(statusEffectType))
		{
			//TO DO  - remove Debug
			Debug.Log(unit.gameObject + "has status effect: " + statusEffectType);
			return TaskStatus.Success;
		}

		return TaskStatus.Failure;
	}
}