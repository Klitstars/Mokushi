using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityCard", menuName = "Cards/NewUtilityCard", order = 2)]
public class SOUtilityCard : SOCardBase
{
    [Header("CardBaseAttributes")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private Sprite cardImage;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private Utility utilityType;
    [SerializeField] private Equipment equipmentType;

    [Header("CardEffects")]
    [SerializeField] private SOUtilityEffect utilityEffect;
    [SerializeField] private SOOnEquipEffect onEquipEffect;
    [SerializeField] private SOOnUnEquipEffect onUnequipEffect;

    public Equipment EquipmentType { get => equipmentType; }
    public Utility UtilityType { get => utilityType; }
    public SOUtilityEffect UtilityEffect { get => utilityEffect; }
    public SOOnEquipEffect OnEquipEffect { get => onEquipEffect; }
    public SOOnUnEquipEffect OnUnequipEffect { get => onUnequipEffect; }
}