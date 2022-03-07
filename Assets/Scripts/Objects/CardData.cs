using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
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
    [SerializeField] private int currentDangerPoints;
    [SerializeField] private int currentPlayNumber;

    [Header("Neutral Attributes")]
    [SerializeField] private GameObject cardUIObject;

    [Header("Card Effects")]
    [SerializeField] private List<CardEffect> cardEffects;
    
    #region Base Card Properties
    public string CardName { get => cardName; }
    public string CardDescription { get => cardDescription; }
    public Sprite CardForeground { get => cardForeground; }
    public Sprite CardBackground { get => cardBackground; }
    public CardType CardType { get => cardType; }
    public GameObject CardUIOjbect { get => cardUIObject; set => cardUIObject = value; }
    #endregion

    #region Utility Card Properties
    public UtilityType UtilityType { get => utilityType; }
    public Equipment EquipmentType { get => equipmentType; }
    public int UtilityPoints { get => utilityPoints; }
    public bool IsMandatory { get => isMandatory; }
    #endregion

    #region Event Card Properties
    public int MaxDangerPoints { get => maxDangerPoints; }
    public int MaxPlayNumber { get => maxPlayNumber; }
    public int CurrentDangerPoints { get => currentDangerPoints; set => currentDangerPoints = value; }
    public int CurrentPlayNumber { get => currentPlayNumber; set => currentPlayNumber = value; }
    #endregion

    #region Effect Attributes
    public List<CardEffect> CardEffects { get => cardEffects;}
    #endregion

    #region Constructor
    public CardData(SOCardData data)
    {
        cardName = data.CardName;
        cardDescription = data.CardDescription;
        cardForeground = data.CardForeground;
        cardBackground = data.CardBackground;
        cardType = data.CardType;

        utilityType = data.UtilityType;
        equipmentType = data.EquipmentType;
        utilityPoints = data.UtilityPoints;

        maxDangerPoints = data.MaxDangerPoints;
        maxPlayNumber = data.MaxPlayNumber;

        cardEffects = data.CardEffects;

        currentDangerPoints = maxDangerPoints;
        currentPlayNumber = maxPlayNumber;
    }
    #endregion

    #region Utility Card Functions
    public void BeginUtilityEffects()
    {
        IterateThroughEffects();
    }

    public void UpdateDangerPoints(int pointsToChange) => currentDangerPoints -= pointsToChange;
    public void UpdatePlayCount(int pointsToChange) => currentPlayNumber -= pointsToChange;

    protected void IterateThroughEffects()
    {
        EffectHandler newEffectHandler;
        foreach (CardEffect effect in cardEffects)
        {
            if(equipmentType == Equipment.None)
                newEffectHandler = new EffectHandler(effect, false);
            else
                newEffectHandler = new EffectHandler(effect, true);

            newEffectHandler.HandleEffect();
        }
    }
    #endregion

    #region Event Card Function


    public void OnEventStarted()
    {
        currentDangerPoints = maxDangerPoints;
        currentPlayNumber = maxPlayNumber;

        //if (enactAtStart)
        //    EventEffect.InitiateEffect();
    }

    public void OnEventEnded()
    {
        //if (enactAtEnd)
        //    EventEffect.InitiateEffect();
    }
    #endregion
}
