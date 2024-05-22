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
    [SerializeField] private Image _selectedImage;
    [SerializeField] private Image _actionIcon;
    [SerializeField] private TextMeshProUGUI _APCostTextMesh;
    [SerializeField] private TextMeshProUGUI _cooldownTextMesh;
    [SerializeField] private TextMeshProUGUI _PPCostTextMesh;
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
        button.onClick.AddListener(UISoundPlayer.Instance.PlayButtonClickedSound);
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
            _actionIcon.sprite = inIcon;
        }
    }

    public void UpdateCooldownText()
    {
        int value = action.Cooldown;
        if (value > 0)
        {
            _cooldownTextMesh.enabled = true;
            _cooldownTextMesh.text = action.Cooldown.ToString();
        }
        else
        {
            _cooldownTextMesh.enabled = false;
        }
    }

    private void SetAction(ActionBehaviour _action)
    {
        m_action = _action;
        _APCostTextMesh.text = "" + m_action.ActionPointCost;
        _PPCostTextMesh.text = "" + m_action.PowerPointCost;
        SetIcon(m_action.Icon);
    }


    public void SetInteractable(bool isInteractable)
    {
        if (isInteractable == true)
        {
            button.interactable = true;
            _actionIcon.color = new Color(1f, 1f, 1f);

        }
        else
        {
            button.interactable = false;
            _actionIcon.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

}
