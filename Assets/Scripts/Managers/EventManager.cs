using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<CardData> currentEventCards;
    [SerializeField] private int eventDangerModifier = 0;
    [SerializeField] private int eventPlayCountModifier = 0;

    public bool hasGrapplingHook = false;
    private int grapplingHookEventDrawCount = 0;

    public bool hasBrokenKatana = false;
    private bool hasBrokenKatanaBuff = false;

    public List<CardData> CurrentEventCards { get => currentEventCards; set => currentEventCards = value; }
    public void UpdateEventDangerModifier(int changeToModifier) => eventDangerModifier += changeToModifier;
    public void UpdateEventPlayCountModifier(int changeToModifier) => eventPlayCountModifier += changeToModifier;
    
    public void DrawCardsAndUpdateEvents(int eventsToDraw)
    {
        for (int i = 0; i < eventsToDraw; i++)
            DrawCardAndUpdateEvents();
    }

    public void DrawCardAndUpdateEvents()
    {
        CardData newEvent = GameManager.instance.DeckManager.DrawRandomEventCard();

        if (newEvent == null)
            return;

        currentEventCards.Add(newEvent);
        GameManager.instance.CardUIPlayController.AddUIEventCard(newEvent);


        if (hasGrapplingHook && newEvent.CardEffects.Select(x => x.effectType).Contains(EffectTypes.Clue)) 
        {
            grapplingHookEventDrawCount++;
            GrapplingHookCheck(newEvent);
        }

        newEvent.OnEventStarted();
        UpdateEventStats();
        //Need UI Update here.
    }

    public bool CheckPlayUtilityOnEvent(CardData utility, CardData targetEvent)
    {
        if (!CheckEventIsPlayable(targetEvent, utility))
            return false;

        else
        {
            targetEvent.UpdateDangerPoints(utility.UtilityPoints);

            if (targetEvent.CurrentDangerPoints + eventDangerModifier <= 0)
                RemoveEventFromStack(targetEvent);

            targetEvent.UpdatePlayCount(1);
            UpdateEventStats();

            GameManager.instance.UtilityManager.PlayUtilityCard(utility);
            return true;

        }
    }

    public bool CheckPlayEquipmentOnEvent(CardData equipment, CardData targetEvent)
    {
        if (!CheckEventIsPlayable(targetEvent, equipment))
            return false;

        if (equipment.CardType != CardType.Utility || equipment.UtilityType != UtilityType.Equipment)
            return false;

        foreach (CardEffect effect in targetEvent.CardEffects)
            if (effect.effectType == EffectTypes.PayItemToEvent && effect.EffectTarget == equipment.EquipmentType)
                return true;

        foreach (CardEffect effect in targetEvent.CardEffects)
            if (effect.effectType == EffectTypes.PayItemToEvent && effect.EffectTarget == Equipment.Any)
                return true;

        return false;
    }

    //public void SenbonzakuraUtilityDiscard(int cardsToDiscard)
    //{
    //    //Do we need some kind of animation here for the event card cycling?
    //    List<CardData> clueCards = new List<CardData>();

    //    for(int i = 0; i < cardsToDiscard; i++)
    //    {
    //        IEnumerable<CardData> eventCards = DrawEventCard();

    //        foreach (CardData card in eventCards)
    //            if (card.CardEffect != EffectTypes.Clue)
    //                GameManager.instance.DeckManager.AddCardToEventDeck(card);
    //            else
    //                clueCards.Add(card);
    //    }

    //    //Clue Cards go somewhere else
    //    GameManager.instance.HandManagerInstance.AddCardToHand(clueCards);
    //}

    public void GrapplingHookUtilityDiscard(CardData eventToDiscard)
    {
        RemoveEventFromStack(eventToDiscard);
    }

    public void EndTurnCheck(bool nullifyDamage)
    {
        List<CardData> eventsToRemove = new List<CardData>();

        foreach (CardData eventCard in currentEventCards)
        {
            int healthDamage = eventCard.CurrentDangerPoints + eventDangerModifier;
            if (!nullifyDamage)
                GameManager.instance.UpdatePlayerHealth(-healthDamage);

            eventsToRemove.Add(eventCard);
        }

        Debug.Log(eventsToRemove.Count + " events to remove.");

        foreach (CardData eventCard in eventsToRemove)
            RemoveEventFromStack(eventCard);

        BrokenKatanaReset();
    }

    public void RemoveClue(CardData clueCard)
    {
        GameManager.instance.UpdateClueCount(1);
        RemoveEventFromStack(clueCard);
    }

    private void Start()
    {
        currentEventCards = new List<CardData>();
        EffectHandler.OnStatsChanged += UpdateEventStats;
        GameManager.OnStartNewTurn += ResetGrapplingCount;
        GameManager.OnStartNewTurn += BrokenKatanaCheck;
    }

    public void RemoveEventFromStack(CardData eventToRemove)
    {
        if (eventToRemove == null)
            return;

        Debug.Log("Removing " + eventToRemove.CardName + " event from stack.");

        eventToRemove.OnEventEnded();

        GameManager.instance.CardUIPlayController.RemoveEventFromStack(eventToRemove);
        currentEventCards.Remove(eventToRemove);
    }

    private bool CheckEventIsPlayable(CardData targetEvent, CardData targetUtility)
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
        
        

        if (targetUtility.UtilityType == UtilityType.Equipment)
            return false;

        return true;
    }

    private void UpdateEventStats()
    {
        foreach(CardData card in currentEventCards)
        {
            GameManager.instance.CardUIPlayController.UpdateEventCardUI(
                card, card.CurrentDangerPoints + eventDangerModifier, card.CurrentPlayNumber + eventPlayCountModifier);
        }            
    }

    private void ResetGrapplingCount()
    {
        grapplingHookEventDrawCount = 0;
    }

    private void GrapplingHookCheck(CardData newEvent)
    {
        if(hasGrapplingHook)
        {
            if (grapplingHookEventDrawCount == 2)
                GameManager.instance.CardUIPlayController.GrapplingHookEventCheck(
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
