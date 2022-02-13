using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayController : MonoBehaviour
{
    private SOEventCard currentEvent;
    private SOUtilityCard currentUtility;

    public void UpdateCurrentEvent(SOEventCard card)
    {
        currentEvent = card;
        card.CardUIOjbect.GetComponent<CardObject>().isPickedUp = true;
        PlayCards();
    }

    public void UpdateCurrentUtility(SOUtilityCard card)
    {
        currentUtility = card;
        card.CardUIOjbect.GetComponent<CardObject>().isPickedUp = true;
        PlayCards();
    }

    private void PlayCards()
    {
        if (currentUtility == null || currentEvent == null)
            return;

        if(GameManager.instance.EventManager.UpdateEventDangerPoints(currentUtility, currentEvent))
            GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility, currentEvent);

        currentUtility.CardUIOjbect.GetComponent<CardObject>().isPickedUp = false;
        currentEvent.CardUIOjbect.GetComponent<CardObject>().isPickedUp = false;

        currentUtility = null;
        currentEvent = null;
    }
}
