using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    static class GameManager
    {
        //fields:
        private static List<Unit> playerUnits;
        private static List<Unit> enemyUnits;
        private static int sceneIndex;
        private static EncounterConfig encounterConfig;



        public static List<Unit> PlayerUnits { get => playerUnits; }
        public static List<Unit> EnemyUnits { get => enemyUnits; }



        static GameManager()
        {
            playerUnits = new();
            enemyUnits = new();
        }



        public static void LoadEncounter()
        {
            LoadEncounterConfig();
            LoadPlayerUnits();
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }



        private static void LoadPlayerUnits()
        {
            playerUnits.Clear();

            Unit unit = Resources.Load("Player Hero 1", typeof(Unit)) as Unit;
            playerUnits.Add(unit);

            unit = Resources.Load("Player Hero 2", typeof(Unit)) as Unit;
            playerUnits.Add(unit);

            unit = Resources.Load("Player Hero 3", typeof(Unit)) as Unit;
            playerUnits.Add(unit);
        }



        private static void LoadEncounterConfig()
        {
            encounterConfig = Resources.Load("Encounters/Debug Preset Encounter Config", typeof(EncounterConfig)) as EncounterConfig;
            if (encounterConfig == null)
            {
                Debug.Log("<color=brown>Unable to Load Encounter Config</color>");
                return;
            }
            sceneIndex = encounterConfig.GetSceneIndex();
            enemyUnits = encounterConfig.GetEnemyUnits();
        }


    }
}