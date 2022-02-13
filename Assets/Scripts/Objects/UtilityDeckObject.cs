using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UtilityDeckObject
{
    [SerializeField] private List<SOUtilityCard> cardDeck;
    [SerializeField] private CardType deckType;

    public List<SOUtilityCard> CardDeck { get => cardDeck; set => cardDeck = value; }
    public CardType DeckType { get => deckType; }
    
    public void RemoveCard(SOUtilityCard card)
    {
        if (!cardDeck.Contains(card))
            return;

        cardDeck.Remove(card);
    }

    public void AddCard(SOUtilityCard card) => cardDeck.Add(card);
}
