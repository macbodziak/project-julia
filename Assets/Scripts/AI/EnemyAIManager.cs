using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using UnityEngine;

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

    private ActionBehaviour ChooseAction(Unit unit)
    {
        if (unit.ActionPoints <= 0)
        {
            return null;
        }
        if (unit.GetActionList().Count == 0)
        {
            return null;
        }
        //TO DO - here use a complex mechanism for selecting best availalbe action
        //TO DO - make sure unit has enough AP for given action
        return unit.GetActionList().First();
    }

    private void StartAction(ActionBehaviour action)
    {
        //TO DO: determine target list
        //TO DO: a better callback for startr ACtion? 

        action.StartAction(ChooseTargets(action), OnActionCompleted);
    }

    // TO DO is this really necassary?
    private void OnActionCompleted()
    {

    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.2f);
        enemies.Clear();
        enemies.AddRange(CombatEncounterManager.Instance.GetEnemyUnitList());
        ActionBehaviour action;
        foreach (Unit enemy in enemies)
        {
            BehaviorTree BT = enemy.GetComponent<BehaviorTree>();
            if (BT != null)
            {
                BT.EnableBehavior();
            }
            else
            {
                Debug.Log("no BehaviorTree");
            }
            // action = ChooseAction(enemy);
            // while (action != null)
            // {
            //     StartAction(action);
            //     yield return new WaitForSeconds(action.GetDuration() + 0.2f);
            //     if (enemyWon)
            //     {
            //         break;
            //     }
            //     action = ChooseAction(enemy);
            // }
            // if (enemyWon)
            // {
            //     break;
            // }
        }

        if (!enemyWon)
        {
            TurnManager.Instance.EndTurn();
        }
    }

    private List<Unit> ChooseTargets(ActionBehaviour action)
    {
        //TO DO - implement proper logic, right now just one random target 
        //TO DO - should account for unmber of targets / targeting mode
        List<Unit> allPlayerUnits = CombatEncounterManager.Instance.GetPlayerUnitList();
        int i = UnityEngine.Random.Range(0, CombatEncounterManager.Instance.GetPlayerUnitList().Count);
        List<Unit> returnList = new();
        returnList.Add(allPlayerUnits[i]);
        return returnList;
    }

    private void HandleEncounterOver(object sender, EncounterOverEventArgs eventArgs)
    {
        enemyWon = !eventArgs.PlayerWon;
    }
}
