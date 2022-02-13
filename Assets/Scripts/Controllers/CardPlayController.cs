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
        PlayCards();
    }

    public void UpdateCurrentUtility(SOUtilityCard card)
    {
        currentUtility = card;

        PlayCards();
    }

    private void PlayCards()
    {
        if (currentUtility == null || currentEvent == null)
            return;

        GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility, currentEvent);

        currentUtility = null;
        currentEvent = null;
    }
}
