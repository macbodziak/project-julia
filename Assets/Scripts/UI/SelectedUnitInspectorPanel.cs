using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectedUnitInspectorPanel : MonoBehaviour
{
    [SerializeField] private PercentageBar healthbar;
    [SerializeField] private TextMeshProUGUI Name_TextMesh;
    [SerializeField] private TextMeshProUGUI AP_TextMesh;
    [SerializeField] private TextMeshProUGUI PP_TextMesh;
    private Unit _unit;

    public Unit unit { get => _unit; }

    public void Setup(Unit unit)
    {
        _unit = unit;

        Name_TextMesh.text = unit.Name;

        healthbar.ShowText = true;
        UpdateHealthBar(unit);

        UpdateActionPoints(unit);
        PP_TextMesh.text = "PP: " + unit.combatStats.CurrentPowerPoints;
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateHealthBar(Unit unit)
    {
        healthbar.Text = unit.combatStats.CurrentHealthPoints + "  /  " + unit.combatStats.MaxHealthPoints;
        healthbar.SetProgress(unit.combatStats.CurrentHealthPoints, unit.combatStats.MaxHealthPoints);
    }

    public void UpdateActionPoints(Unit unit)
    {
        AP_TextMesh.text = "AP: " + unit.combatStats.CurrentActionPoints;
    }
}
