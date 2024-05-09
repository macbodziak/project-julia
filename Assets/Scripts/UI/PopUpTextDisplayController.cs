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

        StatusEffectController.AnyUnitImmuneToStatusEffectEvent += HandleAnyUnitImmuneToStatusEffect;
        StatusEffectController.AnyUnitReceivedStatusEffectEvent += HandleAnyUnitReceivedStatusEffect;
        StatusEffectController.AnyUnitRemovedStatusEffectEvent += HandlAnyUnitRemovedStatusEffect;
        StatusEffectController.AnyUnitSavedFromStatusEffectEvent += HandleAnyUnitSavedFromStatusEffect;
    }

    private void HandleMouseEnterAnyUnit(object sender, EventArgs e)
    {
        Unit unit = (Unit)sender;

        string text = "HP " + unit.CurrentHealthPoints + "/" + unit.combatStats.MaxHealthPoints + "\nAP " + unit.ActionPoints + "\n";
        List<StatusEffectBehaviour> statusEffects = unit.statusEffectController.GetStatusEffects();
        foreach (StatusEffectBehaviour statusEffect in statusEffects)
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

    private void HandleAnyUnitImmuneToStatusEffect(object sender, StatusEffect statusEffect)
    {
        StatusEffectController controller = (StatusEffectController)sender;
        string textToShow = "Immune to " + statusEffect.Name;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, controller.gameObject, GetRandomPosition(), 90f, 1.2f);
    }
    private void HandleAnyUnitSavedFromStatusEffect(object sender, StatusEffect statusEffect)
    {
        StatusEffectController controller = (StatusEffectController)sender;
        string textToShow = "Resisted " + statusEffect.Name;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, controller.gameObject, GetRandomPosition(), 90f, 1.2f);
    }
    private void HandleAnyUnitReceivedStatusEffect(object sender, StatusEffect statusEffect)
    {

        StatusEffectController controller = (StatusEffectController)sender;
        string textToShow = "" + statusEffect.Name;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, controller.gameObject, GetRandomPosition(), 90f, 1.2f);
    }
    private void HandlAnyUnitRemovedStatusEffect(object sender, StatusEffect statusEffect)
    {
        StatusEffectController controller = (StatusEffectController)sender;
        string textToShow = "" + statusEffect.Name;

        TextMeshProUGUI textMesh = textDrawer.DisplayRaisingTextAtGameObject(textToShow, controller.gameObject, GetRandomPosition(), 90f, 1.2f);

        textMesh.fontStyle = FontStyles.Strikethrough;
    }

    private Vector2 GetRandomPosition()
    {
        float random_x = UnityEngine.Random.Range(-15f, 15f);
        float random_y = UnityEngine.Random.Range(0f, 40f);
        return new Vector2(random_x, random_y);
    }
}
