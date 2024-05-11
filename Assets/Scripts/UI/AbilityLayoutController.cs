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
            ActionButton newActionButton = Instantiate(buttonPrefab, transform);
            newActionButton.action = action;
            newActionButton.button.onClick.AddListener(() => { ActionManager.Instance.SelectedAction = action; });
            if (ActionManager.Instance.SelectedUnit.ActionPoints < action.ActionPointCost)
            {
                newActionButton.button.interactable = false;
            }
            else
            {
                newActionButton.button.interactable = true;
            }
            buttonList.Add(newActionButton);
        }
    }

    public void RefreshAbilityList()
    {
        foreach (ActionButton actionButton in buttonList)
        {
            if (ActionManager.Instance.SelectedUnit.ActionPoints < actionButton.action.ActionPointCost)
            {
                actionButton.button.interactable = false;
            }
            else
            {
                actionButton.button.interactable = true;
            }
        }
    }

    public void SetInteractable(bool _interactable)
    {
        if (_interactable == false)
        {
            foreach (ActionButton actionButton in buttonList)
            {
                actionButton.button.interactable = false;
            }
        }
        else
        {
            RefreshAbilityList();
        }
    }
}
