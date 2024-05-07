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
    [SerializeField] private int currentActionPoints;
    [SerializeField] private int dodge;

    [Space(20)]
    [Header("Damage Resistance:")]
    [Space(6)]
    [Range(-100, 100)] public int PhysicalResistance;
    [Range(-100, 100)] public int FireResistance;
    [Range(-100, 100)] public int IceResistance;
    [Range(-100, 100)] public int ElectricResistance;
    [Range(-100, 100)] public int PoisionResistance;

    [Space(20)]
    [Header("Modifiers:")]
    [Space(6)]
    [SerializeField] public float DamageMultiplier = 1.0f;
    [SerializeField] public int DodgeModifier = 0;
    [SerializeField] private int actionPointsModifier = 0;
    [SerializeField] public int HitChanceModifier = 0;
    [SerializeField] public int CritChanceModifier = 0;
    // [SerializeField]
    // [SerializeField]
    // [SerializeField]

    public int CurrentActionPoints { get => currentActionPoints; set => currentActionPoints = value; }
    public int CurrentHealthPoints { get => currentHealthPoints; private set => currentHealthPoints = value; }
    public int Dodge { get => dodge; private set => dodge = value; }
    public int MaxActionPoints { get => maxActionPoints; private set => maxActionPoints = value; }
    public int MaxHealthPoints { get => maxHealthPoints; private set => maxHealthPoints = value; }
    public int ActionPointsModifier
    {
        get => actionPointsModifier;
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

    public void ReceiveAttack(AttackInfo attack)
    {
        bool isCritical = false;
        //make hit roll 
        int hitRoll = UnityEngine.Random.Range(0, 100);
        int requiredRoll = 100 - attack.HitChance + dodge + DodgeModifier;
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

        if (amount > MaxHealthPoints - CurrentHealthPoints)
        {
            amount = MaxHealthPoints - CurrentHealthPoints;
        }
        CurrentHealthPoints += amount;

        OnAnyUnitReceivedHealing?.Invoke(this, new HealingReceivedEventArgs(amount));
    }

    public void ResetActionPoints()
    {
        currentActionPoints = maxActionPoints + ActionPointsModifier;
    }

    private void SetActionPointModifier(int value)
    {
        actionPointsModifier = value;
        currentActionPoints += value;
        currentActionPoints = Mathf.Clamp(currentActionPoints, 0, 10);
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
