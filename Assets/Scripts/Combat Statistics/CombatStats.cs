using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatStats : MonoBehaviour
{
    [Header("Basic Stats:")]
    [Space(6)]
    [SerializeField] private int maxHealthPoints;
    [SerializeField] private int currentHealthPoints;
    [SerializeField] private int maxActionPoints;
    [SerializeField][ReadOnly] private int currentActionPoints;
    [SerializeField] private int maxPowerPoints;
    [SerializeField][ReadOnly] private int currentPowerPoints;
    [SerializeField] private int dodge;

    const int CRIT_MULTIPLIER = 2;

    [Space(12)]
    [Header("Damage Resistance:")]
    [Space(6)]
    [SerializeField] EnumMappedArray<int, DamageType> damageResistanceValues;

    [Space(12)]
    [Header("Status Effect Savinng Throws:")]
    [Space(6)]
    [SerializeField] EnumMappedArray<int, StatusEffectType> statusEffectSaveValues;
    [Space(12)]
    [Header("Modifiers:")]
    [Space(6)]
    [SerializeField] public float DamageMultiplier = 1.0f;
    [SerializeField] public int DodgeModifier = 0;
    [SerializeField] private int _actionPointsModifier = 0;
    [SerializeField] public int HitChanceModifier = 0;
    [SerializeField] public int CritChanceModifier = 0;

    [SerializeField] public EnumMappedArray<int, DamageType> damageResistanceModifiers;
    // <summary>
    // Modifier flags are like boolean, but since several status effects might have the same effect
    // we use a counter instead of bool so one effect does not canceld out another one too early
    // </summary>
    [Space(6)]
    [Header("Modifier Flags:")]
    [Space(6)]
    [SerializeField] public int NoActionPointsRefresh = 0;

    public int CurrentActionPoints { get => currentActionPoints; set => currentActionPoints = value; }
    public int CurrentHealthPoints { get => currentHealthPoints; private set => currentHealthPoints = value; }
    public int Dodge { get => dodge; private set => dodge = value; }
    public int MaxActionPoints { get => maxActionPoints; private set => maxActionPoints = value; }
    public int MaxHealthPoints { get => maxHealthPoints; private set => maxHealthPoints = value; }
    public int MaxPowerPoints { get => maxPowerPoints; private set => maxPowerPoints = value; }
    public int CurrentPowerPoints { get => currentPowerPoints; set => currentPowerPoints = value; }
    public int ActionPointsModifier
    {
        get => _actionPointsModifier;
        set
        {
            SetActionPointModifier(value);
        }
    }


    public static event EventHandler<DamageTakenEventArgs> OnAnyUnitTookDamage;
    public static event EventHandler<HealingReceivedEventArgs> OnAnyUnitReceivedHealing;

    void Awake()
    {
        CurrentHealthPoints = MaxHealthPoints;
        CurrentActionPoints = MaxActionPoints;
        CurrentPowerPoints = MaxPowerPoints;
    }

    public int ApplyResistance(int damageAmount, DamageType damageType)
    {
        float totalResitance = GetTotalDamageResistance(damageType);
        totalResitance = Mathf.Clamp(totalResitance, -100, 100);
        float modifier = (100f - totalResitance) / 100f;
        return (int)(damageAmount * modifier);
    }

    public int GetRequiredHitRoll(AttackInfo attack)
    {
        int requiredRoll = 100 - attack.HitChance + dodge + DodgeModifier;
        requiredRoll = Mathf.Clamp(requiredRoll, 5, 100);
        return requiredRoll;
    }

    public int GetTotalDamageResistance(DamageType damageType)
    {
        return damageResistanceValues[(int)damageType] + damageResistanceModifiers[(int)damageType];
    }

    public int GetStatusEffectSaveValue(StatusEffectType type)
    {
        return statusEffectSaveValues[(int)type];
    }

    private void OnDeath()
    {
        Animator anim = GetComponent<Animator>();
        //after death we do not care about restricting root motion anymore and it improves death animation
        anim.applyRootMotion = true;
        //TO DO trigger some VFX and SFX on death
        anim.SetTrigger("Die");

        //disable collider so the unit cannot be clicked anymore
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        GetComponent<StatusEffectController>().Clear();
    }

    private void OnDodge()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Dodge");
    }

    public bool ReceiveAttack(AttackInfo attack)
    {
        bool isCritical = false;
        //make hit roll 
        int hitRoll = UnityEngine.Random.Range(0, 100);
        int requiredRoll = GetRequiredHitRoll(attack);

        Debug.Log(gameObject + " : required Roll: <color=#ffa8a8>" + requiredRoll + "</color> actual Roll: <color=#ffa8a8>" + hitRoll + "</color>");

        //if hit was successful
        if (hitRoll >= requiredRoll)
        {
            int damageReceived = UnityEngine.Random.Range(attack.MinDamage, attack.MaxDamage);
            Debug.Log(gameObject + " : Damage Roll: <color=#ffa8a8>" + damageReceived + "</color>");

            //check if is critical
            int critRoll = UnityEngine.Random.Range(0, 100);
            int requiredCritRoll = 100 - attack.CritChance;

            if (critRoll >= requiredCritRoll)
            {
                isCritical = true;
                damageReceived *= CRIT_MULTIPLIER;
            }
            TakeDamage(damageReceived, attack.Type, isCritical, true);
            return true;
        }
        else
        {
            OnDodge();
            return false;
        }
    }

    public void ReceiveHealing(HealingInfo healing)
    {
        int amount = UnityEngine.Random.Range(healing.MinAmount, healing.MaxAmount);

        if (amount > MaxHealthPoints - CurrentHealthPoints)
        {
            amount = MaxHealthPoints - CurrentHealthPoints;
        }
        CurrentHealthPoints += amount;

        OnAnyUnitReceivedHealing?.Invoke(this, new HealingReceivedEventArgs(amount));
    }

    public void ResetActionPoints()
    {
        if (NoActionPointsRefresh <= 0)
        {
            currentActionPoints = maxActionPoints + ActionPointsModifier;
        }
    }

    public void ResetPowerPoints()
    {
        currentPowerPoints = maxPowerPoints;
    }

    private void SetActionPointModifier(int value)
    {
        _actionPointsModifier = value;
        currentActionPoints += value;
        currentActionPoints = Mathf.Clamp(currentActionPoints, -5, 5);
    }

    public void TakeDamage(int damage, DamageType damageType, bool isCritical, bool playAnimation = true)
    {
        damage = ApplyResistance(damage, damageType);
        CurrentHealthPoints -= damage;

        if (CurrentHealthPoints <= 0)
        {
            CurrentHealthPoints = 0;
            OnDeath();
        }
        else
        {
            if (playAnimation == true)
            {
                PlayDamageTakenAnimation();
            }
        }

        OnAnyUnitTookDamage?.Invoke(this, new DamageTakenEventArgs(damage, damageType, isCritical, CurrentHealthPoints <= 0));
    }

    private void PlayDamageTakenAnimation()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("HitReaction");
    }
}
