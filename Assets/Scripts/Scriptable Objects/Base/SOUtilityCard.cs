using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityCard", menuName = "Cards/NewUtilityCard", order = 2)]
public class SOUtilityCard : SOCardBase
{
    [Header("Utility Attributes")]
    [SerializeField] protected UtilityType utilityType;
    [SerializeField] protected Equipment equipmentType;
    [SerializeField] protected int utilityPoints;

    [Header("Card Effects")]
    [SerializeField] protected List<SOUtilityEffect> utilityEffects;

    public bool IsMandatory = false;

    public UtilityType UtilityType { get => utilityType; }
    public Equipment EquipmentType { get => equipmentType; }
    public int UtilityPoints { get => utilityPoints; }
    public List<SOUtilityEffect> UtilityEffects { get => utilityEffects; }

    public void PlayUtilityCard()
    { 
        if(utilityType == UtilityType.Equipment)
        {
            UtilityManager.OnEquipItem += OnEquipEffect;
            UtilityManager.OnUnequipItem += OnUnequipEffect;
            return;
        }

        else
        {
            IterateThroughEffects();
            GameManager.OnStartNewTurn += IterateThroughEffectCancellations;
        }
    }

    protected void IterateThroughEffects()
    {
        foreach (SOUtilityEffect effect in utilityEffects)
            effect.InitiateEffect();
    }

    protected void IterateThroughEffectCancellations()
    {
        foreach (SOUtilityEffect effect in utilityEffects)
            effect.CancelEffects();

        GameManager.OnStartNewTurn -= IterateThroughEffectCancellations;
    }

    public void OnEquipEffect()
    {
        IterateThroughEffects();
    }

    public void OnUnequipEffect()
    {
        IterateThroughEffectCancellations();

        UtilityManager.OnEquipItem -= OnEquipEffect;
        UtilityManager.OnUnequipItem -= OnUnequipEffect;
    }
}