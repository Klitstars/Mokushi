using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<SOEventCard> currentEventCards;
    [SerializeField] private int eventDangerModifier = 0;
    [SerializeField] private int eventPlayCountModifier = 0;

    public List<SOEventCard> CurrentEventCards { get => currentEventCards; set => currentEventCards = value; }
    public void UpdateEventDangerModifier(int changeToModifier) => eventDangerModifier += changeToModifier;
    public void UpdateEventPlayCountModifier(int changeToModifier) => eventPlayCountModifier += changeToModifier;
    
    public void DrawAndUpdateEvents(int eventsToDraw)
    {
        for(int i = 0; i < eventsToDraw; i++)
        {
            List<SOEventCard> newCards = new List<SOEventCard>((List<SOEventCard>)GameManager.instance.DeckManager.DrawRandomEventCards());

            if (newCards.Count == 0)
                return;

            foreach(SOEventCard card in newCards)
            {
                currentEventCards.Add(card);
                GameManager.instance.CardBuilder.GenerateCard(card);
                card.OnEventStarted();
            }            
        }

        //Need UI Update here.
    }

    public void DrawAndUpdateEvents()
    {
        SOEventCard newEvent = GameManager.instance.DeckManager.DrawRandomEventCard();
        GameManager.instance.CardBuilder.GenerateCard(newEvent);

        currentEventCards.Add(newEvent);
        newEvent.OnEventStarted();

        //Need UI Update here.
    }

    public bool UpdateEventDangerPoints(SOUtilityCard utility, SOEventCard targetEvent)
    {
        if (!CheckEventIsPlayable(targetEvent))
            return false;

        //Need UI Update here.

        targetEvent.UpdateDangerPoints(utility.UtilityPoints);

        if (targetEvent.CurrentDangerPoints + eventDangerModifier <= 0)
            RemoveEvent(targetEvent);

        UpdatePlayCount(targetEvent);
        return true;
    }

    public void DiscardEventCards(int cardsToDiscard)
    {
        //Do we need some kind of animation here for the event card cycling?
        List<SOCardBase> clueCards = new List<SOCardBase>();

        for(int i = 0; i < cardsToDiscard; i++)
        {
            IEnumerable<SOEventCard> eventCards = DrawEventCard();

            foreach(SOEventCard card in eventCards)
                if (card.EventType != EventCardType.Clue)
                    GameManager.instance.DeckManager.AddCard(card);
                else
                    clueCards.Add(card);
        }

        //Clue Cards go somewhere else
        //GameManager.instance.HandManagerInstance.AddCardToHand(clueCards);
    }

    public void EndTurnCheck(bool nullifyDamage)
    {
        foreach (SOEventCard eventCard in currentEventCards)
        {
            if (!nullifyDamage)
                GameManager.instance.UpdatePlayerHealth(-eventCard.CurrentDangerPoints + eventDangerModifier);

            RemoveEvent(eventCard);
        }
    }

    private void Start()
    {
        currentEventCards = new List<SOEventCard>();
    }

    private IEnumerable<SOEventCard> DrawEventCard()
    {
        return GameManager.instance.DeckManager.DrawRandomEventCards(1);
    }

    private void RemoveEvent(SOEventCard eventToRemove)
    {
        eventToRemove.OnEventEnded();
        Destroy(eventToRemove.CardUIOjbect);
        currentEventCards.Remove(eventToRemove);
    }

    private void UpdatePlayCount(SOEventCard card)
    {
        card.UpdatePlayCount(1);
    }

    private bool CheckEventIsPlayable(SOEventCard targetEvent)
    {
        if (!currentEventCards.Contains(targetEvent))
        {
            Debug.Log("Failed to update Event Card because it is not a current event.");
            return false;
        }

        if (targetEvent.CurrentPlayNumber >= targetEvent.MaxPlayNumber + eventPlayCountModifier)
        {
            Debug.Log("Failed to update Event Card because the Max Play Number is: " + (targetEvent.MaxPlayNumber + eventPlayCountModifier)
                + " and the Current Play Number is: " + targetEvent.CurrentPlayNumber);
            return false;
        }

        return true;
    }
}
