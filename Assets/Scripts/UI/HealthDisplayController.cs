using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// Summary
// This component should be attached to an Canvas and will display Text When mouse is hovering over a unit
public class HealthDisplayController : MonoBehaviour
{
    TextMeshProUGUI dislayedText;
    TextToCanvasDrawer textDrawer;

    private void Awake()
    {
        textDrawer = GetComponent<TextToCanvasDrawer>();
    }
    private void Start()
    {
        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;

        Unit.OnAnyUnitTookDamage += HandleAnyUnitTookDamage;
    }

    private void HandleMouseEnterAnyUnit(object sender, EventArgs e)
    {
        Unit unit = (Unit)sender;
        string text = "HP " + unit.CurrentHealthPoints + "\nAP " + unit.ActionPoints;
        dislayedText = textDrawer.DisplayTextAtGameObject(text, unit.gameObject, new Vector2(0, -50));
    }

    private void HandleMouseExitAnyUnit(object sender, EventArgs e)
    {
        Destroy(dislayedText.gameObject);
    }

    private void HandleAnyUnitTookDamage(object sender, EventArgs e)
    {
        DamageTakenEventArgs args = (DamageTakenEventArgs)e;
        Unit unit = (Unit)sender;
        textDrawer.DisplayRaisingTextAtGameObject("-" + args.Damage, unit.gameObject, new Vector2(0f, 50f), 90f, 1.2f);
    }
}
