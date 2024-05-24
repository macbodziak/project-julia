using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public abstract class EncounterConfig : ScriptableObject
    {

        public abstract int GetSceneIndex();

        public abstract List<Unit> GetEnemyUnits();
    }
}