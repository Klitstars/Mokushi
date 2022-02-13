using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private EventDeckObject eventDeck;
    [SerializeField] private UtilityDeckObject utilityDeck;

    public IEnumerable<SOEventCard> DrawRandomEventCard(int cardsToDraw = 1)
    {
        List<SOEventCard> drawnCards = new List<SOEventCard>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            if (eventDeck.CardDeck.Count == 0)
                break;

            SOEventCard drawnCard = eventDeck.CardDeck[0];
            
            drawnCards.Add(drawnCard);

            //WE NEED TO ADD A DISCARD DECK HERE

            eventDeck.RemoveCard(drawnCard);
        }

        //AnnounceDrawEditor(drawnCards);
        return drawnCards;
    }

    public IEnumerable<SOUtilityCard> DrawRandomUtilityCard(int cardsToDraw = 1)
    {
        List<SOUtilityCard> drawnCards = new List<SOUtilityCard>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            if (utilityDeck.CardDeck.Count == 0)
                break;

            SOUtilityCard drawnCard = utilityDeck.CardDeck[0];

            drawnCards.Add(drawnCard);

            //WE NEED TO ADD A DISCARD DECK HERE

            utilityDeck.RemoveCard(drawnCard);
        }

        return drawnCards;
    }

    public void RandomizeCardDecks()
    {
        List<SOEventCard> newEventDeckOrder = new List<SOEventCard>();
        int deckCount = eventDeck.CardDeck.Count;

        for(int i = 0; i < deckCount; i++)
        {
            int randomInt = Random.Range(0, eventDeck.CardDeck.Count);

            newEventDeckOrder.Add(eventDeck.CardDeck[randomInt]);
            eventDeck.RemoveCard(eventDeck.CardDeck[randomInt]);
        }

        eventDeck.CardDeck = newEventDeckOrder;

        List<SOUtilityCard> newUtilityDeckOrder = new List<SOUtilityCard>();
        deckCount = utilityDeck.CardDeck.Count;

        for (int i = 0; i < deckCount; i++)
        {
            int randomInt = Random.Range(0, utilityDeck.CardDeck.Count);
            newUtilityDeckOrder.Add(utilityDeck.CardDeck[randomInt]);
            utilityDeck.RemoveCard(utilityDeck.CardDeck[randomInt]);
        }

        utilityDeck.CardDeck = newUtilityDeckOrder;
    }

    public void AddCard(SOEventCard cardToAdd) => eventDeck.AddCard(cardToAdd);
    public void AddCard(SOUtilityCard cardToAdd) => utilityDeck.AddCard(cardToAdd);

    private void Start()
    {
        RandomizeCardDecks();
    }
}
