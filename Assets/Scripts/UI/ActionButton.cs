using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionButton : MonoBehaviour
{
    string actionName;
    public BaseAction action;
    public Button button;
    [SerializeField] Image selectedImage;





    private void Awake()
    {
        button = GetComponent<Button>();

        ActionManager.Instance.SelectedActionChangedEvent += handleSelectedActionChanged;
    }

    public string ActionName
    {

        set { actionName = value; GetComponentInChildren<TMPro.TextMeshProUGUI>().text = value; }
        get { return actionName; }
    }

    public void SetSelectedVisual(bool isSelected)
    {
        selectedImage.enabled = isSelected;
    }

    public void handleSelectedActionChanged(object sender, EventArgs e)
    {
        if (ActionManager.Instance.SelectedAction == null)
        {
            SetSelectedVisual(false);
            return;
        }

        if (ActionManager.Instance.SelectedAction.Name() == actionName)
        {
            SetSelectedVisual(true);
        }
        else
        {
            SetSelectedVisual(false);
        }
    }

    private void OnDestroy()
    {
        ActionManager.Instance.SelectedActionChangedEvent -= handleSelectedActionChanged;
    }
    // to be implemented later:
    // public void SetImage();
}
