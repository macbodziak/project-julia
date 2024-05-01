using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatEncounterManager : MonoBehaviour
{
    [SerializeField] List<Unit> playerUnits;
    [SerializeField] List<Unit> enemyUnits;

    private static CombatEncounterManager _instance;
    public event EventHandler<EncounterOverEventArgs> EncounterOverEvent;

    public static CombatEncounterManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            InitializeOnAwake();
        }
    }

    private void InitializeOnAwake()
    {
        // inititlization code comes here
    }

    private void Start()
    {
        Unit.OnAnyUnitTookDamage += HandleAnyUnitTookDamage;
    }


    public int GetEnemyCount()
    {
        return enemyUnits.Count;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnits;
    }

    public List<Unit> GetPlayerUnitList()
    {
        return playerUnits;
    }


    private void HandleAnyUnitTookDamage(object sender, DamageTakenEventArgs eventArgs)
    {
        Unit unit = (Unit)sender;

        if (eventArgs.IsKillingBlow)
        {
            if (unit.IsPlayer)
            {
                RegisterPlayerCharacterDeath(unit);
            }
            else
            {
                RegisterEnemyDeath(unit);
            }
        }
    }

    private void RegisterEnemyDeath(Unit unit)
    {
        enemyUnits.Remove(unit);
        if (enemyUnits.Count == 0)
        {
            OnEncounterOver(true);
            Debug.Log("All enemies are dead");
        }
    }

    private void RegisterPlayerCharacterDeath(Unit unit)
    {
        playerUnits.Remove(unit);
        if (playerUnits.Count == 0)
        {
            OnEncounterOver(false);
            Debug.Log("All player characters are dead");
        }
    }

    private void OnEncounterOver(bool playerWon)
    {
        if (playerWon)
        {
            EncounterOverEvent?.Invoke(this, new EncounterOverEventArgs(true));
            Debug.Log("Player Won");
        }
        else
        {
            EncounterOverEvent?.Invoke(this, new EncounterOverEventArgs(false));
            Debug.Log("Player Lost");
        }
    }
}
