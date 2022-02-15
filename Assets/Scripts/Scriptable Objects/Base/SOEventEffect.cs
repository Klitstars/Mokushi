using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityEffect", menuName = "Effects/NewEventEffect", order = 2)]
public class SOEventEffect : SOEventEffectBase
{
    [Header("Base Effect Attributes")]
    [SerializeField] private EventEffectTypes effectType;
    [SerializeField] private int effectMagnitude;
    [SerializeField] private Equipment effectTarget;

    public EventEffectTypes EffectType { get => effectType; }
    public Equipment EffectTarget { get => effectTarget; }

    public void InitiateEffect()
    {
        DetermineEffect(effectMagnitude);
    }

    public void CancelEffects()
    {
        DetermineCancelation(-effectMagnitude);
    }

    private void DetermineEffect(int effectMagnitude)
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
                DestroyEquippedItem(effectTarget);
                return;
        }
    }

    private void DetermineCancelation(int effectMagnitude)
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

    private void DestroyEquippedItem(Equipment effectTarget)
    {
        if (effectTarget == Equipment.None)
            GameManager.instance.UtilityManager.DestroyEquippedItem();
        
        if (GameManager.instance.UtilityManager.CurrentEquipment != null && GameManager.instance.UtilityManager.CurrentEquipment.EquipmentType == effectTarget)
        {
            GameManager.instance.UtilityManager.DestroyEquippedItem();
            Debug.Log("Destroying Equipment");
        }
    }

    private void PayItemToEvent(Equipment effectTarget)
    {
        //Event that accepts EquipmentCard as payment.
    }
}
