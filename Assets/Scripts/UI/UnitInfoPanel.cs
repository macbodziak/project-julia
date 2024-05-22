using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInspector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameTextMesh;
    [SerializeField] private GameObject statusEffectPanel;
    [SerializeField] private PercentageBar healthBar;
    private Unit _unit;

    public Unit unit { get => _unit; }


    public void Setup(Unit unit, bool showTextArg = false)
    {
        _unit = unit;
        unitNameTextMesh.text = unit.Name; ;

        healthBar.ShowText = showTextArg;
        UpdateStats(unit.combatStats);

        UpdateStatusEffects(unit.statusEffectController);
    }


    public void UpdateStats(CombatStats combatStats)
    {
        healthBar.SetProgress(combatStats.CurrentHealthPoints, combatStats.MaxHealthPoints);
        healthBar.Text = combatStats.CurrentHealthPoints + "  /  " + combatStats.MaxHealthPoints;
    }


    public void UpdateStatusEffects(StatusEffectController sec)
    {
        ClearStatusEffects();

        foreach (StatusEffectBehaviour status in sec.GetStatusEffects())
        {
            CreateStatusEffectIcon(status);
        }
    }


    private void ClearStatusEffects()
    {
        int childCount = statusEffectPanel.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(statusEffectPanel.transform.GetChild(i).gameObject);
        }
    }


    private void CreateStatusEffectIcon(StatusEffectBehaviour status)
    {
        GameObject newIcon = new GameObject();
        RectTransform rect = newIcon.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(32, 32);
        newIcon.AddComponent<CanvasRenderer>();
        Image image = newIcon.AddComponent<Image>();
        image.sprite = status.Icon;
        newIcon.transform.SetParent(statusEffectPanel.transform);
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
