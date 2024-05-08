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

    [SerializeField] EnumMappedArray<Color, DamageType> damageColors;

    private void Start()
    {
        textDrawer = GetComponent<TextToCanvasDrawer>();

        Unit.OnMouseEnterAnyUnit += HandleMouseEnterAnyUnit;
        Unit.OnMouseExitAnyUnit += HandleMouseExitAnyUnit;

        CombatStats.OnAnyUnitTookDamage += HandleAnyUnitTookDamage;
        CombatStats.OnAnyUnitReceivedHealing += HanldeAnyUnitReceivedHealing;
    }

    private void HandleMouseEnterAnyUnit(object sender, EventArgs e)
    {
        Unit unit = (Unit)sender;
        string text = "HP " + unit.CurrentHealthPoints + "\nAP " + unit.ActionPoints + "\n";
        List<StatusEffect> statusEffects = unit.statusEffectController.GetStatusEffects();
        foreach (StatusEffect statusEffect in statusEffects)
        {
            text += statusEffect.Name + "(" + statusEffect.RemainingDuration + ") ";
        }
        dislayedText = textDrawer.DisplayTextAtGameObject(text, unit.gameObject, new Vector2(0, -50));
    }

    private void HandleMouseExitAnyUnit(object sender, EventArgs e)
    {
        Destroy(dislayedText.gameObject);
    }

    private void HandleAnyUnitTookDamage(object sender, EventArgs e)
    {
        DamageTakenEventArgs args = (DamageTakenEventArgs)e;
        CombatStats combatStats = (CombatStats)sender;
        string textToShow = "-" + args.Damage;
        if (args.IsCritical == true)
        {
            textToShow = "CRIT " + textToShow;
        }
        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, combatStats.gameObject, new Vector2(0f, 50f), 90f, 1.2f);
        textMesh.color = damageColors[(int)args.Type];
    }

    private void HanldeAnyUnitReceivedHealing(object sender, HealingReceivedEventArgs args)
    {
        CombatStats combatStats = (CombatStats)sender;
        string textToShow = "+" + args.Amount;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, combatStats.gameObject, new Vector2(0f, 50f), 90f, 1.2f);
        textMesh.color = new Color(0.6f, 1f, 1f, 1f);
    }
}
