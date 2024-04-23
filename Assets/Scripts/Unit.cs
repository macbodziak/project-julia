using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    SelectedVisual selectedVisual;
    [SerializeField] List<BaseAction> actionList;
    [SerializeField] bool isPlayer;

    public static event EventHandler OnAnyUnitDied;
    public static event EventHandler OnMouseEnterAnyUnit;
    public static event EventHandler OnMouseExitAnyUnit;
    [SerializeField] int maxHealthPoints;
    //Debug - Serialized for debugging only
    [SerializeField] int currentHealthPoints;
    public bool IsPlayer
    {
        get { return isPlayer; }
    }

    public int CurrentHealthPoints
    {
        get { return currentHealthPoints; }
    }

    void Awake()
    {
        currentHealthPoints = maxHealthPoints;
        selectedVisual = GetComponent<SelectedVisual>();
        GetComponents<BaseAction>(actionList);
    }


    public void SetSelectionVisual(bool isVisible)
    {
        selectedVisual.SetSelectedVisual(isVisible);
    }

    public List<BaseAction> GetActionList()
    {
        return actionList;
    }

    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            currentHealthPoints = 0;
            OnDeath();
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("HitReaction");
        }
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

        OnAnyUnitDied?.Invoke(this, EventArgs.Empty);
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
