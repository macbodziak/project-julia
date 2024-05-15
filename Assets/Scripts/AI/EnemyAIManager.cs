using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyAIManager : MonoBehaviour
    {

        private static EnemyAIManager _instance;
        private List<Unit> enemies;

        public static EnemyAIManager Instance { get => _instance; }
        private bool enemyWon = false;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            InitializeOnAwake();
        }


        private void InitializeOnAwake()
        {
            enemies = new List<Unit>();
            enemyWon = false;
        }


        private void Start()
        {
            CombatEncounterManager.Instance.EncounterOverEvent += HandleEncounterOver;
        }


        private void OnDestroy()
        {
            if (CombatEncounterManager.Instance != null)
            {
                CombatEncounterManager.Instance.EncounterOverEvent -= HandleEncounterOver;
            }
        }


        public void StartEnemyTurn()
        {
            StartCoroutine(EnemyTurn());
        }


        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(0.2f);
            enemies.Clear();
            enemies.AddRange(CombatEncounterManager.Instance.GetEnemyUnitList());

            foreach (Unit enemy in enemies)
            {
                BehaviorTree BT = enemy.GetComponent<BehaviorTree>();
                if (BT != null)
                {
                    BT.EnableBehavior();
                }
                else
                {
                    Debug.LogWarning("no BehaviorTree");
                }

                while (BT.ExecutionStatus == BehaviorDesigner.Runtime.Tasks.TaskStatus.Running)
                {
                    yield return null;
                }
            }

            if (!enemyWon)
            {
                TurnManager.Instance.EndTurn();
            }
        }


        private void HandleEncounterOver(object sender, EncounterOverEventArgs eventArgs)
        {
            enemyWon = !eventArgs.PlayerWon;
        }
    }
}