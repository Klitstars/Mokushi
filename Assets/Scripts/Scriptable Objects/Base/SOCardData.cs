using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCardTest", menuName = "Cards/NewTestCard", order = 3)]
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
    [SerializeField] private EventCardType eventType;
    [SerializeField] private SOEventEffect eventEffect;
    [SerializeField] private bool enactAtStart;
    [SerializeField] private bool enactAtEnd; 
    
    private int currentDangerPoints;
    private int currentPlayNumber;


    private GameObject cardUIObject;

    #region Base Card Attributes
    public string CardName { get => cardName; }
    public string CardDescription { get => cardDescription; }
    public Sprite CardForeground { get => cardForeground; }
    public Sprite CardBackground { get => cardBackground; }
    public CardType CardType { get => cardType; }
    public GameObject CardUIOjbect { get => cardUIObject; set => cardUIObject = value; }
    #endregion

    #region Utility Card Attributes
    public UtilityType UtilityType { get => utilityType; }
    public Equipment EquipmentType { get => equipmentType; }
    public int UtilityPoints { get => utilityPoints; }
    public bool IsMandatory { get => isMandatory; }
    #endregion

    #region Event Card Attributes
    public EventCardType EventType { get => eventType; }
    public SOEventEffect EventEffect { get => eventEffect; }
    public int MaxDangerPoints { get => maxDangerPoints; }
    public int MaxPlayNumber { get => maxPlayNumber; }
    public int CurrentDangerPoints { get => currentDangerPoints; set => currentDangerPoints = value; }
    public int CurrentPlayNumber { get => currentPlayNumber; set => currentPlayNumber = value; }
    #endregion

}