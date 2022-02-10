using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOCardBase : ScriptableObject
{
    [Header("Card Base")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private Sprite cardImage;
    [SerializeField] private Sprite cardBackground;
    [SerializeField] private CardType cardType;
}