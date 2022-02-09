using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCardBase : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    private Sprite cardImage;
    private Sprite cardBackground;
    private CardType cardType;
}

public enum CardType
{
    Utility,
    Event
}