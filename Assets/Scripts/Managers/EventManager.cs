using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<SOEventCard> currentEventCards;
    [SerializeField] private int eventDangerModifier = 0;
    [SerializeField] private int eventPlayCountModifier = 0;

    public bool hasGrapplingHook = false;
    private int grapplingHookEventDrawCount = 0;

    public bool hasBrokenKatana = false;
    private bool hasBrokenKatanaBuff = false;

    public List<SOEventCard> CurrentEventCards { get => currentEventCards; set => currentEventCards = value; }
    public void UpdateEventDangerModifier(int changeToModifier) => eventDangerModifier += changeToModifier;
    public void UpdateEventPlayCountModifier(int changeToModifier) => eventPlayCountModifier += changeToModifier;
    
    public void DrawCardsAndUpdateEvents(int eventsToDraw)
    {
        for (int i = 0; i < eventsToDraw; i++)
            DrawCardAndUpdateEvents();
    }

    public void DrawCardAndUpdateEvents()
    {
        SOEventCard newEvent = GameManager.instance.DeckManager.DrawRandomEventCard();

        if (newEvent == null)
            return;

        GameManager.instance.CardBuilder.GenerateCard(newEvent);
        currentEventCards.Add(newEvent);
        newEvent.OnEventStarted();

        UpdateEventStats();

        if (hasGrapplingHook && newEvent.EventType != EventCardType.Clue)
        {
            grapplingHookEventDrawCount++;
            GrapplingHookCheck(newEvent);
        }
        //Need UI Update here.
    }

    public bool PlayUtilityOnEvent(SOUtilityCard utility, SOEventCard targetEvent)
    {
        if (!CheckEventIsPlayable(targetEvent, utility))
            return false;

        //Need UI Update here.

        if(targetEvent.EventEffect.EffectType == EventEffectTypes.PayItemToEvent && utility.EquipmentType != Equipment.None)
        {
            GameManager.instance.PlayFieldUIManager.RemoveCardFromHand(utility);
            RemoveEvent(targetEvent);
            
            return true;
        }

        else
        {
            targetEvent.UpdateDangerPoints(utility.UtilityPoints);

            if (targetEvent.CurrentDangerPoints + eventDangerModifier <= 0)
                RemoveEvent(targetEvent);

            targetEvent.UpdatePlayCount(1);
            UpdateEventStats();

            GameManager.instance.UtilityManager.PlayUtilityCard(utility);
            return true;

        }
    }

    public void SenbonzakuraUtilityDiscard(int cardsToDiscard)
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

    public void GrapplingHookUtilityDiscard(SOEventCard eventToDiscard)
    {
        RemoveEvent(eventToDiscard);
    }

    public void EndTurnCheck(bool nullifyDamage)
    {
        List<SOEventCard> eventsToRemove = new List<SOEventCard>();

        foreach (SOEventCard eventCard in currentEventCards)
        {
            int healthDamage = eventCard.CurrentDangerPoints + eventDangerModifier;
            if (!nullifyDamage)
                GameManager.instance.UpdatePlayerHealth(-healthDamage);

            eventsToRemove.Add(eventCard);
        }

        foreach (SOEventCard eventCard in eventsToRemove)
            RemoveEvent(eventCard);

        BrokenKatanaReset();
    }

    public void RemoveClue(SOEventCard clueCard)
    {
        GameManager.instance.UpdateClueCount(1);
        RemoveEvent(clueCard);
    }

    private void Start()
    {
        currentEventCards = new List<SOEventCard>();
        SOUtilityEffect.OnStatsChanged += UpdateEventStats;
        GameManager.OnStartNewTurn += ResetGrapplingCount;
        GameManager.OnStartNewTurn += BrokenKatanaCheck;
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

    private bool CheckEventIsPlayable(SOEventCard targetEvent, SOUtilityCard targetUtility)
    {
        if (!currentEventCards.Contains(targetEvent))
        {
            Debug.Log("Failed to update Event Card because it is not a current event.");
            return false;
        }

        if (targetUtility.IsMandatory)
            return true;
        
        if (targetEvent.CurrentPlayNumber + eventPlayCountModifier <= 0)
        {
            Debug.Log("Failed to update Event Card because the Max Play Number is: " + (targetEvent.MaxPlayNumber + eventPlayCountModifier)
                + " and the Current Play Number is: " + targetEvent.CurrentPlayNumber);
            return false;
        }
        
        if (targetEvent.EventEffect.EffectType == EventEffectTypes.PayItemToEvent && targetUtility.UtilityType == UtilityType.Equipment &&
            targetEvent.EventEffect.EffectTarget == targetUtility.EquipmentType)
            return true;

        if (targetEvent.EventEffect.EffectType == EventEffectTypes.PayItemToEvent && targetUtility.UtilityType == UtilityType.Equipment &&
            targetEvent.EventEffect.EffectTarget == Equipment.Any)
            return true;

        if (targetUtility.UtilityType == UtilityType.Equipment)
            return false;

        return true;
    }

    private void UpdateEventStats()
    {
        foreach(SOEventCard card in currentEventCards)
        {
            GameManager.instance.PlayFieldUIManager.UpdateEventCardUI(
                card, card.CurrentDangerPoints + eventDangerModifier, card.CurrentPlayNumber + eventPlayCountModifier);
        }            
    }

    private void ResetGrapplingCount()
    {
        grapplingHookEventDrawCount = 0;
    }

    private void GrapplingHookCheck(SOEventCard newEvent)
    {
        if(hasGrapplingHook)
        {
            if (grapplingHookEventDrawCount == 2)
                GameManager.instance.PlayFieldUIManager.GrapplingHookEventCheck(
                    newEvent, newEvent.CurrentDangerPoints + eventDangerModifier, newEvent.CurrentPlayNumber + eventPlayCountModifier);
        }        
    }

    private void BrokenKatanaCheck()
    {
        if(hasBrokenKatana)
        {
            int rollChance = Random.Range(1, 11);

            if (rollChance >= 6)
            {
                eventDangerModifier--;
                hasBrokenKatanaBuff = true;
            }
        }

    }

    private void BrokenKatanaReset()
    {
        if (hasBrokenKatanaBuff)
        {
            eventDangerModifier++;
            hasBrokenKatanaBuff = false;
        }
    }
}
