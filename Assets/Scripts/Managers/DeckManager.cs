using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<SOCardData> sOEventDeck;
    [SerializeField] private List<SOCardData> sOUtilityDeck;
    [SerializeField] private List<SOCardData> soEquipmentDeck;
    private DeckObject eventDeck;
    private DeckObject utilityDeck;
    private DeckObject equipmentDeck;

    public List<CardData> EquipmentDeck { get => equipmentDeck.CardDeck; }

    public IEnumerable<CardData> DrawRandomEventCards(int cardsToDraw = 1)
    {
        List<CardData> drawnCards = new List<CardData>();

        for (int i = 0; i < cardsToDraw; i++)
            drawnCards.Add(DrawRandomEventCard());

        return drawnCards;
    }

    public CardData DrawRandomEventCard()
    {
        if (eventDeck.CardDeck.Count == 0)
        {
            Debug.Log("Not enough event cards to draw more!");
            return null;
        }

        CardData drawnCard = eventDeck.CardDeck[0];
        //WE NEED TO ADD A DISCARD DECK HERE
        eventDeck.RemoveCard(drawnCard);

        return drawnCard;
    }

    public IEnumerable<CardData> DrawRandomUtilityCards(int cardsToDraw = 1)
    {
        List<CardData> drawnCards = new List<CardData>();

        for (int i = 0; i < cardsToDraw; i++)
            drawnCards.Add(DrawRandomUtilityCard());

        return drawnCards;
    }

    public CardData DrawRandomUtilityCard()
    {
        if (utilityDeck.CardDeck.Count == 0)
        {
            Debug.Log("Not enough utility cards to draw more!");
            return null;
        }

        CardData drawnCard = utilityDeck.CardDeck[0];
        utilityDeck.RemoveCard(drawnCard);

        //WE NEED TO ADD DISCARD DECK HANDLING HERE


        return drawnCard;
    }

    public void RandomizeCardDecks()
    {
        List<CardData> newEventDeckOrder = new List<CardData>();
        int deckCount = eventDeck.CardDeck.Count;


        for(int i = 0; i < deckCount; i++)
        {
            int randomInt = Random.Range(0, eventDeck.CardDeck.Count);

            newEventDeckOrder.Add(eventDeck.CardDeck[randomInt]);
            eventDeck.RemoveCard(eventDeck.CardDeck[randomInt]);
        }

        eventDeck.CardDeck = newEventDeckOrder;

        List<CardData> newUtilityDeckOrder = new List<CardData>();
        deckCount = utilityDeck.CardDeck.Count;


        for (int i = 0; i < deckCount; i++)
        {
            int randomInt = Random.Range(0, utilityDeck.CardDeck.Count);
            newUtilityDeckOrder.Add(utilityDeck.CardDeck[randomInt]);
            utilityDeck.RemoveCard(utilityDeck.CardDeck[randomInt]);
        }

        utilityDeck.CardDeck = newUtilityDeckOrder;
    }

    public void AddCardToEventDeck(CardData cardToAdd) => eventDeck.AddCard(cardToAdd);
    public void AddCardToUtilityDeck(CardData cardToAdd) => utilityDeck.AddCard(cardToAdd);

    private void Start()
    {
        InitCardDecks();
        GenerateUtilityCards();
        GenerateEventCards();
        GenerateEquipmentCards();
        RandomizeCardDecks();
    }

    private void AddCardToEquipmentDeck(CardData cardToAdd) => equipmentDeck.AddCard(cardToAdd);

    private void GenerateUtilityCards()
    {
        CardData newCard;

        foreach (SOCardData soCard in sOUtilityDeck)
        { 
            newCard = new CardData(soCard);
            AddCardToUtilityDeck(newCard);
        }
    }

    private void GenerateEventCards()
    {
        CardData newCard;

        foreach(SOCardData soCard in sOEventDeck)
        {
            newCard = new CardData(soCard);
            AddCardToEventDeck(newCard);
        }
    }

    private void GenerateEquipmentCards()
    {
        CardData newCard;
        
        foreach(SOCardData soCard in soEquipmentDeck)
        {
            newCard = new CardData(soCard);
            AddCardToEquipmentDeck(newCard);
        }
    }

    private void InitCardDecks()
    {
        utilityDeck = new DeckObject();
        utilityDeck.InitDeckList();

        eventDeck = new DeckObject();
        eventDeck.InitDeckList();

        equipmentDeck = new DeckObject();
        equipmentDeck.InitDeckList();
    }
}
