using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    //TO DO - remove serializefield later
    //Serialized for Debugging only
    [SerializeField][ReadOnly] private List<ActionBehaviour> actionList;
    private Unit unit;

    private void Start()
    {
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
    public bool IsActionAvailable(ActionBehaviour action)
    {
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
