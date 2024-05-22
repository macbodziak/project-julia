using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortraitBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image healthImage;
    private Unit _unit;



    public Unit unit { get => _unit; }


    public static event EventHandler AnyPortraitMouseEnterEvent;
    public static event EventHandler AnyPortraitMouseExitEvent;


    public void SetFillFactor(float fillFactor)
    {
        healthImage.fillAmount = fillFactor;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        AnyPortraitMouseEnterEvent?.Invoke(this, EventArgs.Empty);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        AnyPortraitMouseExitEvent?.Invoke(this, EventArgs.Empty);
    }


    public void Setup(Unit unit)
    {
        healthImage.fillAmount = 0f;

        _unit = unit;
        if (unit.Portrait != null)
        {
            GetComponent<Image>().sprite = unit.Portrait;
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Default_portait");
        }
        CombatStats.AnyUnitTookDamageEvent += HandleAnyUnitHealthChanged;
        CombatStats.AnyUnitReceivedHealingEvent += HandleAnyUnitHealthChanged;
    }


    private void HandleAnyUnitHealthChanged(object sender, EventArgs eventArgs)
    {
        CombatStats cs = sender as CombatStats;
        if (cs.GetComponent<Unit>() != unit)
        {
            return;
        }

        SetFillFactor(1.0f - (float)cs.CurrentHealthPoints / (float)cs.MaxHealthPoints);

        DamageTakenEventArgs e = eventArgs as DamageTakenEventArgs;
        if (e != null)
        {
            Debug.Log("damage taken..");
            if (e.IsKillingBlow)
            {
                Destroy(this.gameObject, 0.5f);
            }
        }
    }


    private void OnDestroy()
    {
        CombatStats.AnyUnitTookDamageEvent -= HandleAnyUnitHealthChanged;
        CombatStats.AnyUnitReceivedHealingEvent -= HandleAnyUnitHealthChanged;
    }

}
