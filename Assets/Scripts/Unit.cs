using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System;

[RequireComponent(typeof(DamageResistance), typeof(StatusEffectController))]
public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    public static event EventHandler<DamageTakenEventArgs> OnAnyUnitTookDamage;
    public static event EventHandler<HealingReceivedEventArgs> OnAnyUnitReceivedHealing;
    public static event EventHandler OnMouseEnterAnyUnit;
    public static event EventHandler OnMouseExitAnyUnit;
    [SerializeField] int maxHealthPoints;
    int currentHealthPoints;

    [SerializeField] int maxActionPoints;
    [SerializeField] int actionPoints;

    [SerializeField] int dodge;

    DamageResistance damageResistance;
    StatusEffectController statusEffectController;

    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    public int CurrentHealthPoints
    {
        get { return currentHealthPoints; }
    }

    public int ActionPoints
    {
        get { return actionPoints; }
        set { actionPoints = value; }
    }

    void Awake()
    {
        currentHealthPoints = maxHealthPoints;
        ActionPoints = maxActionPoints;

        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<BaseAction>(actionList);
        damageResistance = GetComponent<DamageResistance>();
        statusEffectController = GetComponent<StatusEffectController>();
    }


    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<BaseAction> GetActionList()
    {
        return actionList;
    }

    public void ReceiveAttack(AttackInfo attack)
    {
        bool isCritical = false;
        //make hit roll 
        int hitRoll = UnityEngine.Random.Range(0, 100);
        int requiredRoll = 100 - attack.HitChance + dodge;
        requiredRoll = Mathf.Clamp(requiredRoll, 5, 100);
        Debug.Log($"Rolling attack dice: {hitRoll} vs {requiredRoll}");

        //if hit was successful
        if (hitRoll >= requiredRoll)
        {
            int damageReceived = UnityEngine.Random.Range(attack.MinDamage, attack.MaxDamage);
            Debug.Log($"Rolling Damage dice: {damageReceived}");
            //check if is critical
            int critRoll = UnityEngine.Random.Range(0, 100);
            int requiredCritRoll = 100 - attack.CritChance;
            Debug.Log($"Rolling Crit dice: {critRoll} vs {requiredCritRoll}");

            if (critRoll >= requiredCritRoll)
            {
                isCritical = true;
                damageReceived *= 2;
                Debug.Log($"Damage after crit: {damageReceived}");
            }
            //account for resistance
            damageReceived = damageResistance.ApplyResistance(damageReceived, attack.Type);
            Debug.Log($"Final damage after resitance: {damageReceived}");

            TakeDamage(damageReceived, isCritical);
        }
        else
        {
            OnDodge();
        }

    }

    public void ReceiveHealing(HealingInfo healing)
    {
        int amount = UnityEngine.Random.Range(healing.MinAmount, healing.MaxAmount);

        if (amount > maxHealthPoints - currentHealthPoints)
        {
            amount = maxHealthPoints - currentHealthPoints;
        }
        currentHealthPoints += amount;

        OnAnyUnitReceivedHealing?.Invoke(this, new HealingReceivedEventArgs(amount));
    }

    public void ReceiveStatusEffect<T>() where T : StatusEffect
    {
        statusEffectController.ReceiveStatusEffect<T>();
    }

    public void RemoveStatusEffect<T>() where T : StatusEffect
    {
        statusEffectController.RemoveStatusEffect<T>();
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            currentHealthPoints = 0;
            OnDeath();
        }
        else
        {
            OnDamageTaken(damage);
        }

        OnAnyUnitTookDamage?.Invoke(this, new DamageTakenEventArgs(damage, isCritical, currentHealthPoints <= 0));
    }

    private void OnDeath()
    {
        Debug.Log(gameObject + "  DIED");
        Animator anim = GetComponent<Animator>();
        //after death we do not vare avout restrivting root motion anymore and it improves death animation
        anim.applyRootMotion = true;
        //TO DO trigger some VFX and SFX on death
        anim.SetTrigger("Die");

        //disable collider so the unit cannot be clicked anymore
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void OnDamageTaken(int damage)
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("HitReaction");
    }

    private void OnDodge()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Dodge");
    }

    public void ResetActionPoints()
    {
        ActionPoints = maxActionPoints;
    }

    private void OnMouseEnter()
    {
        OnMouseEnterAnyUnit?.Invoke(this, EventArgs.Empty);
    }

    private void OnMouseExit()
    {
        OnMouseExitAnyUnit?.Invoke(this, EventArgs.Empty);
    }
}
