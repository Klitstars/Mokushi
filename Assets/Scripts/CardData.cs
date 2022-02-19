using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
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

    private GameObject cardUIObject;

    private int currentDangerPoints;
    private int currentPlayNumber;

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

    #region Utility Card Functions
    public void PlayUtilityCard()
    {
        if (utilityType == UtilityType.Equipment)
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
        //foreach (SOUtilityEffect effect in utilityEffects)
        //    effect.InitiateEffect();
    }

    protected void IterateThroughEffectCancellations()
    {
        //foreach (SOUtilityEffect effect in utilityEffects)
        //    effect.CancelEffects();

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
    #endregion

    #region Event Card Function
    public void UpdateDangerPoints(int pointsToChange) => currentDangerPoints -= pointsToChange;
    public void UpdatePlayCount(int pointsToChange) => currentPlayNumber -= pointsToChange;

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
