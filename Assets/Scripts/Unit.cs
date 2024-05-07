using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System;

[RequireComponent(typeof(StatusEffectController))]
public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    public static event EventHandler<DamageTakenEventArgs> OnAnyUnitTookDamage;
    public static event EventHandler<HealingReceivedEventArgs> OnAnyUnitReceivedHealing;
    public static event EventHandler OnMouseEnterAnyUnit;
    public static event EventHandler OnMouseExitAnyUnit;

    [Header("----Basic Stats-----------")]
    [SerializeField] int maxHealthPoints;
    int currentHealthPoints;

    [SerializeField] int maxActionPoints;
    [SerializeField] int actionPoints;

    [SerializeField] int dodge;

    [Header("--- Damage Resistance ---")]
    [Range(-100, 100)] public int PhysicalResistance;
    [Range(-100, 100)] public int FireResistance;
    [Range(-100, 100)] public int IceResistance;
    [Range(-100, 100)] public int ElectricResistance;
    [Range(-100, 100)] public int PoisionResistance;
    // CombatStats combatStats;
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
    }

    private void Start()
    {
        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<BaseAction>(actionList);
        // combatStats = GetComponent<CombatStats>();
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

        //if hit was successful
        if (hitRoll >= requiredRoll)
        {
            int damageReceived = UnityEngine.Random.Range(attack.MinDamage, attack.MaxDamage);
            //check if is critical
            int critRoll = UnityEngine.Random.Range(0, 100);
            int requiredCritRoll = 100 - attack.CritChance;

            if (critRoll >= requiredCritRoll)
            {
                isCritical = true;
                damageReceived *= 2;
            }
            TakeDamage(damageReceived, attack.Type, isCritical, true);
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

    public void TakeDamage(int damage, DamageType damageType, bool isCritical, bool playAnimation = true)
    {
        damage = ApplyResistance(damage, damageType);
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            currentHealthPoints = 0;
            OnDeath();
        }
        else
        {
            if (playAnimation == true)
            {
                PlayDamageTakenAnimation();
            }
        }

        OnAnyUnitTookDamage?.Invoke(this, new DamageTakenEventArgs(damage, damageType, isCritical, currentHealthPoints <= 0));
    }

    private void OnDeath()
    {
        Animator anim = GetComponent<Animator>();
        //after death we do not vare avout restrivting root motion anymore and it improves death animation
        anim.applyRootMotion = true;
        //TO DO trigger some VFX and SFX on death
        anim.SetTrigger("Die");

        //disable collider so the unit cannot be clicked anymore
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        statusEffectController.Clear();
    }

    private void PlayDamageTakenAnimation()
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

    public int ApplyResistance(int damageAmount, DamageType damageType)
    {
        float modifier = (100f - GetResistanceValue(damageType)) / 100f;
        return (int)(damageAmount * modifier);
    }

    public int GetResistanceValue(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                return PhysicalResistance;
            case DamageType.Fire:
                return FireResistance;
            case DamageType.Ice:
                return IceResistance;
            case DamageType.Electric:
                return ElectricResistance;
            case DamageType.Poision:
                return PoisionResistance;
            default:
                return 0;
        }
    }
}
