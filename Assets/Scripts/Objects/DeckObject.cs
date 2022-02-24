using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckObject
{
    [SerializeField] private List<CardData> cardDeck;

    public List<CardData> CardDeck { get => cardDeck; set => cardDeck = value; }

    public void RemoveCard(CardData card)
    {
        if (!cardDeck.Contains(card))
            return;

        cardDeck.Remove(card);
    }

    public void AddCard(CardData card) => cardDeck.Add(card);
    public void AddCardToTop(CardData card) => cardDeck.Insert(0, card);

    public void InitDeckList()
    {
        cardDeck = new List<CardData>();
    }
}
