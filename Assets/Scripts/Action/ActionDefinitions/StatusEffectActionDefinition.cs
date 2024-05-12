using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Apply and Remove Effects Action Definition", menuName = "Scriptable Objects/Actions/Apply and Remove Effects Action Definition", order = 202)]
public class StatusEffectActionDefinition : ActionDefinition
{
    [SerializeField] private List<StatusEffect> _statusEffectsApplied;
    [SerializeField] private List<StatusEffect> _statusEffectsRemoved;

    public List<StatusEffect> StatusEffectsApplied { get => _statusEffectsApplied; protected set => _statusEffectsApplied = value; }
    public List<StatusEffect> StatusEffectsRemoved { get => _statusEffectsRemoved; protected set => _statusEffectsRemoved = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            VisualEffect vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);
            Destroy(vfx.gameObject, 2.5f);

            //remove status effects if this action definition has status effects
            RemoveStatusEffects(target, StatusEffectsRemoved);

            //apply status effects if this action definition has status effects
            ApplyStatusEffects(target, StatusEffectsApplied);
        }
    }

}