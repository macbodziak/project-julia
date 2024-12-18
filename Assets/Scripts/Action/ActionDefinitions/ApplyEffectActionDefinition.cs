using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Apply Effect Action Definition", menuName = "Scriptable Objects/Actions/Apply Effect Action Definition", order = 200)]
public class ApplyEffectActionDefinition : ActionDefinition
{
    [SerializeField] private List<StatusEffectDurationInfo> _statusEffectsApplied;

    public List<StatusEffectDurationInfo> StatusEffectsApplied { get => _statusEffectsApplied; protected set => _statusEffectsApplied = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            //apply status effects if this action definition has status effects
            ApplyStatusEffects(target, StatusEffectsApplied);
            //TO DO - if hit
            GameObject vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);

        }
    }

}