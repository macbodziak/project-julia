using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;


namespace EnemyAI
{

    [TaskCategory("My Tasks/Target Selection")]
    [TaskDescription("Sekect Self as Target")]
    [TaskName("Select Self as Target")]
    public class SelectSelfAsTarget : Action
    {
        [SerializeField] SharedUnitList selectedTargets;
        Unit unit;


        public override void OnAwake()
        {
            unit = GetComponent<Unit>();
        }


        public override TaskStatus OnUpdate()
        {

            List<Unit> target = new List<Unit> { unit };
            selectedTargets.Value = target;
            return TaskStatus.Success;
        }
    }
}