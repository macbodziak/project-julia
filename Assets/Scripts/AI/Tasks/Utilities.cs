using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;



namespace EnemyAI
{
    public static class Utilities
    {
        public static bool UnitHasDamageResistanceBelow(Unit unit, SharedActionBehaviour actionBehaviour, int damageResistance)
        {
            ICanAttack attackAction = actionBehaviour.Value.actionDefinition as ICanAttack;
            if (attackAction != null)
            {
                if (unit.combatStats.GetTotalDamageResistance(attackAction.DamageType) < damageResistance)
                {
                    return true;
                }
            }

            return false;
        }



        //<summary>
        //This formula adjusts the health points by factoring in damage resistance and dodge chance to simulate how much damage needs to be dealt to kill target.
        //HP_left / (1 -  damage resistance [%]) * (1 + dodge [%])
        //HP_threshold - above that HP makes no difference, example if set to 50, if a unit has 60 HP and another has 70, 
        //  they will be treated the same and only Dodge and Resistance will be factored in
        //The lower the score the easier it should be to kill the target and thus should be biased to be selected
        //</summary>
        public static float Score(Unit target, SharedActionBehaviour selectedAction, int HP_threshold = 50)
        {
            ICanAttack attackAction = selectedAction.Value.actionDefinition as ICanAttack;

            float HP_left = Mathf.Clamp(target.combatStats.CurrentHealthPoints, 0f, HP_threshold);

            float damage_resistance = 0;
            if (attackAction != null)
            {
                damage_resistance = target.combatStats.GetTotalDamageResistance(attackAction.DamageType) / 100f;
            }
            if (damage_resistance >= 1f)
            {
                damage_resistance = 0.9999f;
            }

            float dodge = target.combatStats.TotalDodge / 100f;


            float score = HP_left / (1f - damage_resistance) * (1 + dodge);
            return score;
        }

        //<summary>
        //as input, this method takes a list of units and a corresponding list of score values
        //it checks which score value is the highets and returns the respective unit (based on the list index)
        //scoring should be done before calling this method
        //</summary>
        public static Unit SelectTargetByScore(List<Unit> targetList, List<int> scores)
        {
            if (targetList.Count == 0)
            {
                return null;
            }
            int selectedIndex = 0;
            for (int i = 1; i < targetList.Count; i++)
            {
                if (scores[i] > scores[selectedIndex])
                {
                    selectedIndex = i;
                }
                else if (scores[i] == scores[selectedIndex])
                {
                    if (UnityEngine.Random.value > 0.5f)
                    {
                        selectedIndex = i;
                    }
                }
            }

            return targetList[selectedIndex];
        }
    }
}