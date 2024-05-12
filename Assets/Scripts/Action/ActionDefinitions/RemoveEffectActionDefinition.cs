using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Remove Effect Action Definition", menuName = "Scriptable Objects/Actions/Remove Effect Action Definition", order = 201)]
public class RemoveEffectActionDefinition : ActionDefinition
{
    [SerializeField] private List<StatusEffect> _statusEffectsRemoved;

    public List<StatusEffect> StatusEffectsRemoved { get => _statusEffectsRemoved; protected set => _statusEffectsRemoved = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            //remove status effects if this action definition has status effects
            RemoveStatusEffects(target, StatusEffectsRemoved);

            VisualEffect vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);
            if (vfx != null)
            {
                Destroy(vfx.gameObject, 2.5f);
            }
        }
    }

}
