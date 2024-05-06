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
    public static CombatEncounterManager Instance { get { return _instance; } }
    public event EventHandler<EncounterOverEventArgs> EncounterOverEvent;

    public bool IsEncounterOver { get; private set; }

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
        IsEncounterOver = false;
    }

    private void Start()
    {
        Unit.OnAnyUnitTookDamage += HandleAnyUnitTookDamage;

        //DEBUG - for testing only
        playerUnits[1].ReceiveStatusEffect<BurningStatusEffect>();
        playerUnits[1].ReceiveStatusEffect<BleedingStatusEffect>();
        playerUnits[0].ReceiveStatusEffect<BleedingStatusEffect>();
        //end of testing code
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
        }
    }

    private void RegisterPlayerCharacterDeath(Unit unit)
    {
        playerUnits.Remove(unit);
        if (playerUnits.Count == 0)
        {
            OnEncounterOver(false);
        }
    }

    private void OnEncounterOver(bool playerWon)
    {
        InputManager.Instance.SetState(InputState.Blocked);
        IsEncounterOver = true;

        if (playerWon)
        {
            EncounterOverEvent?.Invoke(this, new EncounterOverEventArgs(true));
        }
        else
        {
            EncounterOverEvent?.Invoke(this, new EncounterOverEventArgs(false));
        }
    }
}
