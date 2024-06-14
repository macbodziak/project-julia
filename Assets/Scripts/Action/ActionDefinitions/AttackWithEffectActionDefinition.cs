using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Attack with Status Effect Action Definition", menuName = "Scriptable Objects/Actions/Attack with Effect Action Definition", order = 2)]
public class AttackWithEffectActionDefinition : AttackActionDefinition
{

    [SerializeField] private List<StatusEffectDurationInfo> _statusEffects;

    public List<StatusEffectDurationInfo> StatusEffectsApplied { get => _statusEffects; protected set => _statusEffects = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            Attack attack = GetAttackData(actingUnit, target);
            AttackResult attackResult = target.combatStats.ReceiveAttack(attack, actingUnit);

            if (attackResult.Hit)
            {
                GameObject vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);
                if (target.combatStats.CurrentHealthPoints > 0)
                {
                    ApplyStatusEffects(target, StatusEffectsApplied);
                }
            }
        }
    }

}
