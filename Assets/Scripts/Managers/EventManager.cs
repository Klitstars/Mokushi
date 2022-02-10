using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private SOEventCard currentEvent;
    private int currentEventDanger;
    private int currentEventPlayCount;
    private int eventDangerModifier;
    private int eventPlayCountModifier;

    private DeckController deckController;

    public SOEventCard CurrentEventCard { get => currentEvent; set => currentEvent = value; }
    public int CurrentEventDanger { get => currentEventDanger; }
    public int CurrentEventPlayCount { get => currentEventPlayCount; }
    public int EventDangerModifier { get => EventDangerModifier; }
    public int EventPlayCountModifier { get => eventPlayCountModifier; }
    public void UpdateCurrentEventDanger(int dangerPointsToChange) => currentEventDanger += dangerPointsToChange;
    public void UpdateCurrentPlayCount(int playCountPointsToChange) => currentEventPlayCount += playCountPointsToChange;
    public void UpdateEventDangerModifier(int changeToModifier) => eventDangerModifier += changeToModifier;
    public void UpdateEventPlayCountModifier(int changeToModifier) => eventPlayCountModifier += changeToModifier;


    public delegate void onEventChanged();
    public static event onEventChanged OnEventChanged;

    public void DrawAndUpdateEvent() => currentEvent = (SOEventCard)deckController.DrawRandomCard();

    public void ScavengerEvent()
    {
        //Can destroy current equipment to reduce the danger points to 0.
    }

    public void RogueNinjaEvent()
    {
        //If you have a katana, destroy it after this event.
    }

    public void DiscardEventCards(int cardsToDiscard)
    {
        //Do we need some kind of animation here for the event card cycling?

        for(int i = 0; i < cardsToDiscard; i++)
        {
            SOEventCard card = DrawEventCard();

            if (card.EventType == EventCardType.Clue)
                GameManager.instance.HandManagerInstance.AddCardToHand(card);
            else               
                deckController.AddCard(card);
        }
    }

    public void InfectedEvent()
    {
        UpdateEventDangerModifier(1);
        OnEventChanged += InfectedEventRemoval;
    }

    private void Awake()
    {
        if (deckController == null)
            deckController = GetComponent<DeckController>();

    }

    private void Start() => deckController.RandomizeCardDeck();

    private void InfectedEventRemoval()
    {
        UpdateEventDangerModifier(1);
        OnEventChanged -= InfectedEventRemoval;
    }

    private SOEventCard DrawEventCard()
    {
        return (SOEventCard)deckController.DrawRandomCard();
    }
}
