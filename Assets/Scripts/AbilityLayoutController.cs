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

    public void CreateAndShowAbilityList(List<BaseAction> actions)
    {
        foreach (var action in actions)
        {
            ActionButton newActionButton = Instantiate(buttonPrefab, transform);
            // newButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Name();
            // replace with
            newActionButton.ActionName = action.Name();
            newActionButton.button.onClick.AddListener(() => { ActionManager.Instance.SelectedAction = action; });
            buttonList.Add(newActionButton);
        }
    }
}
