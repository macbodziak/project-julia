using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ActionButton : MonoBehaviour
{
    private ActionBehaviour m_action;
    public Button button;
    [SerializeField] Image _selectedImage;
    [SerializeField] private TextMeshProUGUI _APCostTextMesh;
    [SerializeField] private TextMeshProUGUI _cooldownTextMesh;
    private Sprite m_icon;

    public Sprite icon
    {
        get { return m_icon; }
    }

    public ActionBehaviour action
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
        _selectedImage.enabled = isSelected;
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

    public void UpdateCooldownText()
    {
        int value = action.cooldown;
        if (value > 0)
        {
            _cooldownTextMesh.enabled = true;
            _cooldownTextMesh.text = action.cooldown.ToString();
        }
        else
        {
            _cooldownTextMesh.enabled = false;
        }
    }

    private void SetAction(ActionBehaviour _action)
    {
        m_action = _action;
        _APCostTextMesh.text = "" + m_action.actionPointCost;
        SetIcon(m_action.icon);
    }
}
