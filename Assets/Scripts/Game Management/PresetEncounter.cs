using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    [CreateAssetMenu(fileName = "Preset Encounter Config", menuName = "Scriptable Objects/Encounters/Preset Encounter Config", order = 1)]
    public class PresetEncounter : EncounterConfig
    {
        //fields
        [SerializeField] private int _sceneIndex;
        [SerializeField] private List<Unit> _enemiesList;


        //Properties
        // public int SceneIndex { get => _sceneIndex; }
        // public List<Unit> EnemiesList { get => _enemiesList; }



        //methods
        public override List<Unit> GetEnemyUnits()
        {
            return _enemiesList;
        }


        public override int GetSceneIndex()
        {
            return _sceneIndex;
        }
    }
}