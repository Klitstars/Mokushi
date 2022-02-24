using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardTest", menuName = "Cards/NewCard", order = 3)]
[System.Serializable]
public class SOCardData : ScriptableObject
{
    [Header("Card Base Attributes")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private Sprite cardForeground;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] protected CardType cardType;

    [Header("Utility Attributes")]
    [SerializeField] private UtilityType utilityType;
    [SerializeField] private Equipment equipmentType;
    [SerializeField] private int utilityPoints;
    [SerializeField] private bool isMandatory = false;

    [Header("Event Attributes")]
    [SerializeField] private int maxDangerPoints;
    [SerializeField] private int maxPlayNumber;
    [SerializeField] private bool enactAtStart;
    [SerializeField] private bool enactAtEnd;

    [Header("Effect Attributes")]
    [SerializeField] private List<CardEffect> cardEffects;


    #region Base Card Attributes
    public string CardName { get => cardName; }
    public string CardDescription { get => cardDescription; }
    public Sprite CardForeground { get => cardForeground; }
    public Sprite CardBackground { get => cardBackground; }
    public CardType CardType { get => cardType; }
    #endregion

    #region Utility Card Attributes
    public UtilityType UtilityType { get => utilityType; }
    public Equipment EquipmentType { get => equipmentType; }
    public int UtilityPoints { get => utilityPoints; }
    public bool IsMandatory { get => isMandatory; }
    #endregion

    #region Event Card Attributes
    public int MaxDangerPoints { get => maxDangerPoints; }
    public int MaxPlayNumber { get => maxPlayNumber; }
    public bool EnactAtStart { get => enactAtStart; }
    public bool EnactAtEnd { get => enactAtEnd; }
    #endregion

    #region Effects
    public List<CardEffect> CardEffects { get => cardEffects; }
    #endregion
}