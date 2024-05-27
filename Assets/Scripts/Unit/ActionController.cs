using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private List<ActionBehaviour> actionList;
    [SerializeField][ReadOnly] private ActionBehaviour activeAction;
    private Unit unit;

    private void Start()
    {
        actionList = new();
        GetComponents<ActionBehaviour>(actionList);
        unit = GetComponent<Unit>();
    }


    public List<ActionBehaviour> GetActionList()
    {
        return actionList;
    }


    public List<ActionBehaviour> GetAvailableActions()
    {
        List<ActionBehaviour> availableActions = new();

        foreach (ActionBehaviour action in actionList)
        {
            if (IsActionAvailable(action))
            {
                availableActions.Add(action);
            }
        }

        return availableActions;
    }



    public ActionBehaviour GetActionByName(string actionName)
    {
        foreach (ActionBehaviour action in actionList)
        {
            if (action.actionDefinition.Name == actionName)
            {
                return action;
            }
        }
        return null;
    }


    public bool IsActionAvailable(ActionBehaviour action)
    {
        if (action == null)
        {
            return false;
        }

        if (action.ActionPointCost > unit.ActionPoints)
        {
            return false;
        }

        if (action.Ready == false)
        {
            return false;
        }
        return true;
    }


    public bool RegisterActiveAction(ActionBehaviour action)
    {
        Debug.Log("registering Action " + action.actionDefinition.Name);
        if (activeAction == null)
        {
            activeAction = action;
            return true;
        }
        return false;
    }


    public void UnregisterActiveAction()
    {
        Debug.Log("unregistering Action " + activeAction.actionDefinition.Name);
        activeAction = null;
    }


    private void OnActionAnimationFinished()
    {
        Debug.Log("<color=cyan> OnActionAnimatinFinished</color>");
        if (activeAction != null)
        {
            activeAction.OnActionCompleted();
            UnregisterActiveAction();
        }
    }



    private void OnActionExecutionTriggered()
    {
        activeAction?.ExecuteLogic();
    }
}
