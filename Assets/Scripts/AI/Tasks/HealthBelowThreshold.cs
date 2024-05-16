using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace EnemyAI
{

	[TaskCategory("My Tasks")]
	[TaskDescription("Returns Success if the HP percantage of the unit is below provied threshold.\nThreshold should be between 0 and 1")]
	[TaskIcon("Assets/Sprites/Behaviour Designer Icons/Health_Percentage.png")]
	public class HealthBelowThreshold : Conditional
	{
		[SerializeField] float threshold;
		Unit unit;


		public override void OnAwake()
		{
			unit = GetComponent<Unit>();
		}


		public override TaskStatus OnUpdate()
		{
			float currentRatio = (float)unit.combatStats.CurrentHealthPoints / (float)unit.combatStats.MaxHealthPoints;

			if (currentRatio < threshold)
			{
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}