using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ActionBehaviour m_action;
    public Button button;
    [SerializeField] private Image _selectedImage;
    [SerializeField] private Image _actionIcon;
    [SerializeField] private TextMeshProUGUI _APCostTextMesh;
    [SerializeField] private TextMeshProUGUI _cooldownTextMesh;
    [SerializeField] private TextMeshProUGUI _PPCostTextMesh;
    private Sprite m_icon;

    public static event EventHandler MouseEnterAnyActionButtonEvent;
    public static event EventHandler MouseExitAnyActionButtonEvent;

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
        ActionManager.Instance.SelectedActionChangedEvent += HandleSelectedActionChanged;
    }


    public void SetSelectedVisual(bool isSelected)
    {
        _selectedImage.enabled = isSelected;
    }


    public void HandleSelectedActionChanged(object sender, EventArgs e)
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
        ActionManager.Instance.SelectedActionChangedEvent -= HandleSelectedActionChanged;
    }

    public void SetIcon(Sprite inIcon)
    {
        if (inIcon != null)
        {
            _actionIcon.sprite = inIcon;
        }
        else
        {
            _actionIcon.sprite = Resources.Load<Sprite>("default_warning_icon");
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
        if (button == null)
        {
            button = GetComponent<Button>();
        }
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnterAnyActionButtonEvent?.Invoke(this, EventArgs.Empty);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExitAnyActionButtonEvent?.Invoke(this, EventArgs.Empty);
    }


    public static void ClearAllListeners()
    {
        MouseEnterAnyActionButtonEvent = null;
        MouseExitAnyActionButtonEvent = null;
    }
}
