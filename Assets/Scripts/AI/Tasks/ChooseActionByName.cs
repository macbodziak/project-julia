using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace EnemyAI
{
    [TaskCategory("My Tasks/Action Selection")]
    [TaskDescription("Chooses an action based on its name as defined in the Action Definition Name property.\nReturns Failure if action could not be found or is not available,"
    + " for example not enough action points or still on cooldown. Sets the value of \"Selected Action\"")]
    public class ChooseActionByName : Action
    {
        [SerializeField] string actionName;
        private Unit myUnit;
        private ActionController actionController;
        public SharedActionBehaviour SelectedAction;


        public override void OnAwake()
        {
            myUnit = GetComponent<Unit>();
            actionController = GetComponent<ActionController>();
        }


        public override TaskStatus OnUpdate()
        {
            ActionBehaviour choosenAction = actionController.GetActionByName(actionName);
            if (choosenAction == null)
            {
                return TaskStatus.Failure;
            }

            if (actionController.IsActionAvailable(choosenAction) == false)
            {
                return TaskStatus.Failure;
            }
            //DEBUG
            Debug.Log("selected Action: <color=#ffa8a8>" + choosenAction.actionDefinition.Name + "</color>");

            SelectedAction.Value = choosenAction;
            return TaskStatus.Success;
        }
    }
}