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
    [SerializeField] private string unitName;
    [SerializeField] private List<StatusEffectBehaviour> statusEffects;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxHP;


    public void Setup(string unitNameArg, List<StatusEffectBehaviour> statusEffectsArg, int currentHPArg, int maxHPArg, bool showTextArg = false)
    {
        unitName = unitNameArg;
        unitNameTextMesh.text = unitName;

        healthBar.ShowText = showTextArg;
        UpdateStats(currentHPArg, maxHPArg);

        UpdateStatusEffects(statusEffectsArg);
    }


    public void UpdateStats(int currentHPArg, int maxHPArg)
    {
        currentHP = currentHPArg;
        maxHP = maxHPArg;

        healthBar.SetProgress(currentHP, maxHP);
        healthBar.Text = currentHPArg + "  /  " + maxHPArg;
    }


    public void UpdateStatusEffects(List<StatusEffectBehaviour> statusEffects)
    {
        this.statusEffects = statusEffects;
        ClearStatusEffects();

        foreach (StatusEffectBehaviour status in statusEffects)
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

    // private void HandleMouseOverUnit(object sender, EventArgs e)
    // {
    //     Unit unit = sender as Unit;
    //     gameObject.SetActive(true);
    //     Setup(
    //         unit.Name,
    //         unit.statusEffectController.GetStatusEffects(),
    //         unit.combatStats.CurrentHealthPoints,
    //         unit.combatStats.MaxHealthPoints);
    // }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
