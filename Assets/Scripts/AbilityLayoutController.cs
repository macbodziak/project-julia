using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLayoutController : MonoBehaviour
{
    List<Button> buttonList;
    [SerializeField] Button buttonPrefab;

    private void Awake()
    {
        buttonList = new List<Button>();
    }
    public void ClearList()
    {
        foreach (var button in buttonList)
        {
            Destroy(button.gameObject);
            Debug.Log("ClearList()" + button.gameObject.name);
        }
        buttonList.Clear();
    }

    public void ShowAbilityList(List<BaseAction> actions)
    {
        foreach (var action in actions)
        {
            Button newButton = Instantiate(buttonPrefab, transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = action.Name();
            buttonList.Add(newButton);
        }
    }
}
