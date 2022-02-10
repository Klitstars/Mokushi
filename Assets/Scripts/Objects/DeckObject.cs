using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckObject
{
    [SerializeField] private List<SOCardBase> cardDeck;
    [SerializeField] private CardType deckType;

    public List<SOCardBase> CardDeck { get => cardDeck; set => cardDeck = value; }
    public CardType DeckType { get => deckType; }
    
    public void RemoveCard(SOCardBase card)
    {
        if (!cardDeck.Contains(card))
            return;

        cardDeck.Remove(card);
    }

    public void AddCard(SOCardBase card) => cardDeck.Add(card);
}
