using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private BaseAction ChooseAction(Unit unit)
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

    private void StartAction(BaseAction action)
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
        BaseAction action;
        foreach (Unit enemy in enemies)
        {
            action = ChooseAction(enemy);
            while (action != null)
            {
                StartAction(action);
                yield return new WaitForSeconds(action.GetDuration() + 0.2f);
                if (enemyWon)
                {
                    break;
                }
                action = ChooseAction(enemy);
            }
            if (enemyWon)
            {
                break;
            }
        }

        if (!enemyWon)
        {
            TurnManager.Instance.EndTurn();
        }
    }

    private List<Unit> ChooseTargets(BaseAction action)
    {
        //TO DO - implement
        return CombatEncounterManager.Instance.GetPlayerUnitList();
    }

    private void HandleEncounterOver(object sender, EncounterOverEventArgs eventArgs)
    {
        enemyWon = !eventArgs.PlayerWon;
    }
}
