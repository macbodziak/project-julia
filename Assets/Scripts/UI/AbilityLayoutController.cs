using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLayoutController : MonoBehaviour
{
    List<ActionButton> buttonList;
    [SerializeField] ActionButton buttonPrefab;

    private void Awake()
    {
        buttonList = new List<ActionButton>();
    }
    public void ClearList()
    {
        foreach (var button in buttonList)
        {
            Destroy(button.gameObject);
        }
        buttonList.Clear();
    }

    public void CreateAndShowAbilityList(List<ActionBehaviour> actions)
    {
        foreach (var action in actions)
        {
            if (action.actionDefinition == null)
            {
                continue;
            }
            ActionButton newActionButton = Instantiate(buttonPrefab, transform);
            newActionButton.action = action;
            newActionButton.button.onClick.AddListener(() => { ActionManager.Instance.SelectedAction = action; });
            if (isActionAvailable(action))
            {
                newActionButton.SetInteractable(true);
            }
            else
            {
                newActionButton.SetInteractable(false);
            }
            newActionButton.UpdateCooldownText();
            buttonList.Add(newActionButton);
        }
    }

    public void RefreshAbilityList()
    {
        foreach (ActionButton actionButton in buttonList)
        {
            if (isActionAvailable(actionButton.action))
            {
                actionButton.SetInteractable(true);
            }
            else
            {
                actionButton.SetInteractable(false);
            }
            actionButton.UpdateCooldownText();
        }
    }

    public void SetInteractable(bool _interactable)
    {
        if (_interactable == false)
        {
            foreach (ActionButton actionButton in buttonList)
            {
                actionButton.SetInteractable(false);
            }
        }
        else
        {
            RefreshAbilityList();
        }
    }

    private bool isActionAvailable(ActionBehaviour action)
    {
        return ActionManager.Instance.SelectedUnit.ActionPoints >= action.ActionPointCost
        && ActionManager.Instance.SelectedUnit.PowerPoints >= action.PowerPointCost
        && action.Ready;
    }
}
