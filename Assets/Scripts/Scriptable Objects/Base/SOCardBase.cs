using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCardBase : ScriptableObject
{
    [Header("Card Base Attributes")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private Sprite cardForeground;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] protected CardType cardType;
    private GameObject cardUIObject;

    public string CardName { get => cardName; }
    public string CardDescription { get => cardDescription; }
    public Sprite CardForeground { get => cardForeground; }
    public Sprite CardBackground { get => cardBackground; }
    public CardType CardType { get => cardType; }
    public GameObject CardUIOjbect { get => cardUIObject; set => cardUIObject = value; }

}