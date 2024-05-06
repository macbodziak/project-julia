using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ActionButton : MonoBehaviour
{
    private BaseAction m_action;
    public Button button;
    [SerializeField] Image selectedImage;
    [SerializeField] TextMeshProUGUI APCostText;

    private Sprite m_icon;

    public Sprite icon
    {
        get { return m_icon; }
    }

    public BaseAction action
    {
        get { return m_action; }
        set { SetAction(value); }
    }

    private void Start()
    {
        button = GetComponent<Button>();
        ActionManager.Instance.SelectedActionChangedEvent += handleSelectedActionChanged;
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

        if (ActionManager.Instance.SelectedAction == m_action)
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

    public void SetIcon(Sprite inIcon)
    {
        if (inIcon != null)
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
            button.GetComponent<Image>().sprite = inIcon;
        }
    }

    private void SetAction(BaseAction _action)
    {
        m_action = _action;
        APCostText.text = "" + m_action.ActionPointCost;
        SetIcon(m_action.Icon);
    }
}
