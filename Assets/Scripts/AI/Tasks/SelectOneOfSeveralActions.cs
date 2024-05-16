using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

namespace EnemyAI
{
    [TaskCategory("My Tasks/Action Selection")]
    [TaskDescription("Chooses randomly one of several actions.\nActions are defined by name as defined in the Action Definition Name property.\nReturns Failure if none of the actions could not be found or are not available,"
    + " for example not enough action points or still on cooldown. Sets the value of \"Selected Action\"")]
    public class SelectOneOfSeveralActions : Action
    {
        [SerializeField] List<string> actionNames;
        private ActionController actionController;
        public SharedActionBehaviour SelectedAction;


        public override void OnAwake()
        {
            actionController = GetComponent<ActionController>();
        }


        public override TaskStatus OnUpdate()
        {
            List<ActionBehaviour> givenActions = new List<ActionBehaviour>();

            foreach (string name in actionNames)
            {
                ActionBehaviour temp = actionController.GetActionByName(name);
                if (temp != null)
                {
                    if (actionController.IsActionAvailable(temp))
                    {
                        givenActions.Add(temp);
                    }
                }
            }

            if (givenActions.Count == 0)
            {
                return TaskStatus.Failure;
            }

            int randomIndex = UnityEngine.Random.Range(0, givenActions.Count);
            ActionBehaviour choosenAction = givenActions[randomIndex];

            //DEBUG
            Debug.Log("selected Action: <color=#ffa8a8>" + choosenAction.actionDefinition.Name + "</color>");

            SelectedAction.Value = choosenAction;
            return TaskStatus.Success;
        }
    }
}