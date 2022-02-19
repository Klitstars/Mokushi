using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckObject : MonoBehaviour
{
    [SerializeField] private List<CardData> cardDeck;
    [SerializeField] private CardType deckType;

    public List<CardData> CardDeck { get => cardDeck; set => cardDeck = value; }
    public CardType DeckType { get => deckType; }

    public void RemoveCard(CardData card)
    {
        if (!cardDeck.Contains(card))
            return;

        cardDeck.Remove(card);
    }

    public void AddCard(CardData card) => cardDeck.Add(card);
    public void AddCardToTop(CardData card) => cardDeck.Insert(0, card);
}
