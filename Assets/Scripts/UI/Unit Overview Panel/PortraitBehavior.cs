using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortraitBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Unit _unit;



    public Unit unit { get => _unit; }


    public static event EventHandler AnyPortraitMouseEnterEvent;
    public static event EventHandler AnyPortraitMouseExitEvent;


    public void SetFillFactor(float fillFactor)
    {
        Image image = GetComponent<Image>();
        image.material.SetFloat("_FillAmount", fillFactor);
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
        Image image = GetComponent<Image>();
        _unit = unit;
        if (unit.Portrait != null)
        {
            image.sprite = unit.Portrait;
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Default_portait");
        }

        image.material = new Material(image.material);
        image.material.SetTexture("_BaseMap", image.sprite.texture);

        image.material.renderQueue = 3000;

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
            if (e.IsKillingBlow)
            {
                StartCoroutine(OnDeath(2f));
                Destroy(this.gameObject, 2f);
            }
        }
    }


    private void OnDestroy()
    {
        CombatStats.AnyUnitTookDamageEvent -= HandleAnyUnitHealthChanged;
        CombatStats.AnyUnitReceivedHealingEvent -= HandleAnyUnitHealthChanged;
    }


    private IEnumerator OnDeath(float duration)
    {
        Image image = GetComponent<Image>();
        float startTime = Time.time;
        float delta = 0f;
        while (Time.time - startTime < duration)
        {
            image.material.SetFloat("_DissolveAmount", delta / duration);
            delta = Time.time - startTime;
            yield return null;
        }

        yield return null;
    }
}
