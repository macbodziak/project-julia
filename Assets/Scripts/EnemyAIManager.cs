using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{

    private static EnemyAIManager _instance;
    private List<Unit> enemies;

    public static EnemyAIManager Instnace { get => _instance; }
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

        enemies = new List<Unit>();
    }

    public void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }

    private BaseAction ChooseAction(Unit unit)
    {
        if (unit.ActionPoints == 0)
        {
            return null;
        }
        if (unit.GetActionList().Count == 0)
        {
            return null;
        }
        //TO DO - here use a complex mechanism for selecting best availalbe action
        return unit.GetActionList().First();
    }

    private void StartAction(BaseAction action)
    {
        Debug.Log("starting action " + action);
        //TO DO: determine target list
        //TO DO: a better callback for startr ACtion? 

        action.StartAction(CombatEncounterManager.Instance.GetPlayerUnitList(), CheckWinCondition);
    }

    // private void OnActionCompleted()
    // {

    // }

    private void CheckWinCondition()
    {
        //TO DO check if encounter won / lost
        Debug.Log("TO IMPLEMENT  -  check for win lost condition");
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy turn started...");
        yield return new WaitForSeconds(0.2f);
        // get list aof all enemy units
        enemies.AddRange(CombatEncounterManager.Instance.GetEnemyUnitList());
        BaseAction action;
        foreach (Unit enemy in enemies)
        {
            action = ChooseAction(enemy);
            while (action != null)
            {
                StartAction(action);
                yield return new WaitForSeconds(action.GetDuration() + 0.2f);
                CheckWinCondition();
                action = ChooseAction(enemy);
            }
        }

        TurnManager.Instance.EndTurn();
    }
}
