using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckObject
{
    [SerializeField] private List<SOCardBase> cardDeck;
    [SerializeField] private CardType deckType;

    public List<SOCardBase> CardDeck { get => cardDeck; }
    public CardType DeckType { get => deckType; }
}
