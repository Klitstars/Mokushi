using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventDeckObject
{
    [SerializeField] private List<SOEventCard> cardDeck;
    [SerializeField] private CardType deckType;

    public List<SOEventCard> CardDeck { get => cardDeck; set => cardDeck = value; }
    public CardType DeckType { get => deckType; }
    
    public void RemoveCard(SOEventCard card)
    {
        if (!cardDeck.Contains(card))
            return;

        cardDeck.Remove(card);
    }

    public void AddCard(SOEventCard card) => cardDeck.Add(card);

    public void AddCardToTop(SOEventCard card) => cardDeck.Insert(0, card);
}
