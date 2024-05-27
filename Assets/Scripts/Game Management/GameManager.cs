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
        private static int encounterIndex;
        private static EncounterConfig encounterConfig;



        public static List<Unit> PlayerUnits { get => playerUnits; }
        public static List<Unit> EnemyUnits { get => enemyUnits; }



        static GameManager()
        {
            playerUnits = new();
            enemyUnits = new();
        }



        public static void LoadEncounter(int index)
        {
            LoadEncounterConfig();
            LoadPlayerUnits();
            ClearAllStaticEvents();
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }



        private static void LoadPlayerUnits()
        {
            playerUnits.Clear();

            Unit unit = Resources.Load("Prototypes/Player Hero 1", typeof(Unit)) as Unit;
            playerUnits.Add(unit);

            unit = Resources.Load("Prototypes/Player Hero 2", typeof(Unit)) as Unit;
            playerUnits.Add(unit);

            unit = Resources.Load("Sorcerer", typeof(Unit)) as Unit;
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
            enemyUnits.AddRange(encounterConfig.GetEnemyUnits());
        }



        public static void NewGame()
        {
            playerUnits.Clear();
            enemyUnits.Clear();
            encounterIndex = 0;
            LoadEncounter(encounterIndex);
        }



        public static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running in a standalone build
            Application.Quit();
#endif
        }



        public static void LoadMainMenu()
        {
            playerUnits.Clear();
            enemyUnits.Clear();
            ClearAllStaticEvents();
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }



        private static void ClearAllStaticEvents()
        {
            Unit.ClearAllListeners();
            CombatStats.ClearAllListeners();
            StatusEffectController.ClearAllListeners();
            PortraitBehavior.ClearAllListeners();
        }
    }
}