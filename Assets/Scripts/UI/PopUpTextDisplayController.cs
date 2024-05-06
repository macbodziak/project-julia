using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// <summary>
// This component should be attached to an Canvas and will display Text When mouse is hovering over a unit
// </summary>
public class PopUpTextDisplayController : MonoBehaviour
{
    private TextMeshProUGUI dislayedText;
    private TextToCanvasDrawer textDrawer;

    [SerializeField] Color physicalDamageColor;

    [SerializeField] Color fireDamageColor;

    [SerializeField] Color iceDamageColor;

    [SerializeField] Color electricDamageColor;

    [SerializeField] Color poisonDamageColor;

    private void Awake()
    {
        textDrawer = GetComponent<TextToCanvasDrawer>();
    }
    private void Start()
    {
        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;

        Unit.OnAnyUnitTookDamage += HandleAnyUnitTookDamage;
        Unit.OnAnyUnitReceivedHealing += HanldeAnyUnitReceivedHealing;
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
        string textToShow = "-" + args.Damage;
        if (args.IsCritical == true)
        {
            textToShow = "CRIT " + textToShow;
        }
        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, unit.gameObject, new Vector2(0f, 50f), 90f, 1.2f);
        textMesh.color = GetDamageColor(args.Type);
    }

    private void HanldeAnyUnitReceivedHealing(object sender, HealingReceivedEventArgs args)
    {
        Unit unit = (Unit)sender;
        string textToShow = "+" + args.Amount;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, unit.gameObject, new Vector2(0f, 50f), 90f, 1.2f);
        textMesh.color = new Color(0.6f, 1f, 1f, 1f);
    }

    public Color GetDamageColor(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                return physicalDamageColor;
            case DamageType.Fire:
                return new Color(1f, 0.56f, 0.17f, 1f);
            case DamageType.Ice:
                return new Color(0.38f, 0.71f, 0.99f, 1f);
            case DamageType.Electric:
                return new Color(0.57f, 0.55f, 1f, 1f);
            case DamageType.Poision:
                return new Color(0.55f, 0.77f, 0.22f, 1f);
            default:
                return new Color(0.8f, 0.8f, 0.8f, 1f);

        }
    }
}
