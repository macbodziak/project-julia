using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Heal with Effect Action Definition", menuName = "Scriptable Objects/Actions/Heal with Effect Action Definition", order = 101)]
public class HealWithEffectActionDefinition : ActionDefinition
{
    [SerializeField] private int minAmount;
    [SerializeField] private int maxAmount;
    [SerializeField] private List<StatusEffectDurationInfo> _statusEffectsApplied;
    [SerializeField] private List<StatusEffect> _statusEffectsRemoved;

    public int MinAmount { get => minAmount; protected set => minAmount = value; }
    public int MaxAmount { get => maxAmount; protected set => maxAmount = value; }
    public List<StatusEffectDurationInfo> StatusEffectsApplied { get => _statusEffectsApplied; protected set => _statusEffectsApplied = value; }
    public List<StatusEffect> StatusEffectsRemoved { get => _statusEffectsRemoved; protected set => _statusEffectsRemoved = value; }

    public override void ExecuteLogic(Unit actingUnit, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            target.combatStats.ReceiveHealing(GetHealingInfo());

            // PlayVisualEffect(VisualEffectOnHitPrefab, target.transform.position + new Vector3(0f, 1.2f, 0f));
            GameObject vfx = PlayVisualEffect(VisualEffectOnHitPrefab, target.transform);

            //remove status effects if this action definition has status effects
            RemoveStatusEffects(target, StatusEffectsRemoved);

            //apply status effects if this action definition has status effects
            ApplyStatusEffects(target, StatusEffectsApplied);
        }
    }

    public HealingInfo GetHealingInfo()
    {
        return new HealingInfo(MinAmount, MaxAmount);
    }
}
