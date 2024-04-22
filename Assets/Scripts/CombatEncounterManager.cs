using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEncounterManager : MonoBehaviour
{
    [SerializeField] List<Unit> playerUnits;
    [SerializeField] List<Unit> enemyUnits;

    private static CombatEncounterManager _instance;


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

    public int GetEnemyCount()
    {
        return enemyUnits.Count;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnits;
    }
}
