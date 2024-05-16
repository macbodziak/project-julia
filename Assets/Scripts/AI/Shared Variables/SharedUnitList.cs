using System.Collections.Generic;
using BehaviorDesigner.Runtime;

namespace EnemyAI
{
    [System.Serializable]
    public class SharedUnitList : SharedVariable<List<Unit>>
    {
        public static implicit operator SharedUnitList(List<Unit> value) { return new SharedUnitList { Value = value }; }
    }
}