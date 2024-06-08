using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace GameManagement
{

    public class EncounterManager : MonoBehaviour
    {
        [SerializeField] List<Unit> playerUnits;
        [SerializeField] List<Unit> enemyUnits;
        [SerializeField] List<Transform> playerSpawnPoints;
        [SerializeField] List<Transform> enemySpawnPoints;

        private static EncounterManager _instance;
        public static EncounterManager Instance { get { return _instance; } }
        public event EventHandler<EncounterOverEventArgs> EncounterOverEvent;
        public event EventHandler<EventArgs> EncounterSetupCompleteEvent;

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
            playerUnits = new();
            enemyUnits = new();
            IsEncounterOver = false;
        }


        //<summary>
        // This method fetches the units from the static class Game Manager at the start of the scene initialization
        //</summary>
        private void SetupUnits()
        {
            Unit newUnit;

            for (int i = 0; i < GameManagement.GameManager.PlayerUnits.Count; i++)
            {
                newUnit = Instantiate<Unit>(GameManagement.GameManager.PlayerUnits[i], playerSpawnPoints[i].position, playerSpawnPoints[i].rotation);
                playerUnits.Add(newUnit);
            }

            for (int i = 0; i < GameManagement.GameManager.EnemyUnits.Count; i++)
            {
                newUnit = Instantiate<Unit>(GameManagement.GameManager.EnemyUnits[i], enemySpawnPoints[i].position, enemySpawnPoints[i].rotation);
                enemyUnits.Add(newUnit);
            }
        }



        private void Start()
        {
            CombatStats.AnyUnitTookDamageEvent += HandleAnyUnitTookDamage;
            SetupUnits();

            StartCoroutine(LateStart());
        }



        //<summary>
        // This method gets invoked after all other GamObject have called their Start methods, to ensure that 
        // all listeners had a chance to subscribe to the EncounterSetupCompleteEvent event
        //</summary>
        private IEnumerator LateStart()
        {
            yield return null;
            EncounterSetupCompleteEvent?.Invoke(this, EventArgs.Empty);
            yield return null;
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
            CombatStats combatStats = (CombatStats)sender;
            Unit unit = combatStats.GetComponent<Unit>();

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
}