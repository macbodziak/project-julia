using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private List<ActionBehaviour> actionList;
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
}