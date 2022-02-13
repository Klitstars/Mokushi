using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityEffect", menuName = "Effects/NewEventEffect", order = 2)]
public class SOEventEffect : SOEventEffectBase
{
    [Header("Base Effect Attributes")]
    [SerializeField] private EventEffectTypes effectType;
    [SerializeField] private int effectMagnitude;
    [SerializeField] private EffectTargets effectTarget;

    private int currentDurationTimer = 0;

    public void InitiateEffect()
    {
        DetermineEffect(effectMagnitude, effectTarget);
    }

    public void CancelEffects()
    {
        DetermineCancelation(-effectMagnitude, effectTarget);
    }

    private void DetermineEffect(int effectMagnitude, EffectTargets effectTarget)
    {
        switch (effectType)
        {
            case EventEffectTypes.DamageTakenModifier:
                ModifyDamageTaken(effectMagnitude);
                return;

            case EventEffectTypes.PayItemToEvent:
                PayItemToEvent(effectTarget);
                return;

            case EventEffectTypes.DestroyEquippedItem:
                DestroyEquippedItem();
                return;
        }
    }

    private void DetermineCancelation(int effectMagnitude, EffectTargets effectTarget)
    {
        switch (effectType)
        {
            case EventEffectTypes.DamageTakenModifier:
                ModifyDamageTaken(-effectMagnitude);
                return;

            case EventEffectTypes.PayItemToEvent:
                return;

            case EventEffectTypes.DestroyEquippedItem:
                return;
        }
    }

    private void ModifyDamageTaken(int effectMagnitude)
    {
        GameManager.instance.UpdateDamageModifier(effectMagnitude);
    }

    private void DestroyEquippedItem()
    {
        GameManager.instance.UtilityManager.RemoveEquipment();
    }

    private void PayItemToEvent(EffectTargets effectTarget)
    {
        //Event that accepts EquipmentCard as payment.
    }
}
