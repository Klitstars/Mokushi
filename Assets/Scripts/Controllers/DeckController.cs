using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] private DeckObject deck;

    public IEnumerable<SOCardBase> DrawRandomCard(int cardsToDraw = 1)
    {
        List<SOCardBase> drawnCards = new List<SOCardBase>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            SOCardBase drawnCard = deck.CardDeck[0];
            
            drawnCards.Add(drawnCard);
            
            //WE NEED TO ADD A DISCARD DECK HERE

            deck.RemoveCard(drawnCard);
        }

        AnnounceDrawEditor(drawnCards);
        return drawnCards;
    }

    public void RandomizeCardDeck()
    {
        List<SOCardBase> newDeckOrder = new List<SOCardBase>();
        int deckCount = deck.CardDeck.Count;

        for(int i = 0; i < deckCount; i++)
        {
            int randomInt = Random.Range(0, deck.CardDeck.Count);
            newDeckOrder.Add(deck.CardDeck[randomInt]);
        }

        deck.CardDeck = newDeckOrder;
    }

    public void AddCard(SOCardBase cardToAdd) => deck.AddCard(cardToAdd);

    private void AnnounceDrawEditor(IEnumerable<SOCardBase> cards)
    {
        foreach(SOCardBase card in cards)
        {
            Debug.Log(card.name);
        }
    }
}
