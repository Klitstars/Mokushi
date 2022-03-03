using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayController : MonoBehaviour
{
    private CardData currentEvent;
    private CardData currentUtility;
    public bool equipmentSlotSelected = false;
    public bool mandatoryCardInPlay = false;

    public void UpdateSelectedEvent(CardData card)
    {
        if (equipmentSlotSelected)
            equipmentSlotSelected = false;

        if(currentEvent != null)
            currentEvent.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;

        if (currentEvent == card)
        {
            currentEvent = null;
            return;
        }

        currentEvent = card;
        card.CardUIOjbect.GetComponent<CardUI>().isPickedUp = true;

        if (currentEvent.CardEffects.Select(x => x.effectType).Contains(EffectTypes.Clue))
        {
            GameManager.instance.EventManager.RemoveClue(card);
            GameManager.instance.EventManager.DrawCardAndUpdateEvents();
            currentEvent = null;
            return;
        }

        PlayCards();
    }

    public void UpdateSelectedUtility(CardData card)
    {
        if(currentUtility != null)
            currentUtility.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;

        if (mandatoryCardInPlay && !card.IsMandatory)
            return;

        if (currentUtility == card)
        {
            currentUtility = null;
            return;
        }

        currentUtility = card;
        card.CardUIOjbect.GetComponent<CardUI>().isPickedUp = true;

        PlayCards();
    }

    private void PlayCards()
    {
        if (currentUtility == null)
            return;

        if (currentUtility.EquipmentType != Equipment.None && equipmentSlotSelected)
        {
            GameManager.instance.CardUIPlayController.UpdateEquippedUtility(currentUtility);
            GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility);

            currentUtility.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
            currentUtility = null;
            equipmentSlotSelected = false;

            return;
        }

        if (currentEvent != null)
        {
            if (GameManager.instance.EventManager.CheckPlayUtilityOnEvent(currentUtility, currentEvent))
            {
                currentUtility.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
                currentEvent.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;

            }

            currentUtility.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
            currentUtility = null;

            currentEvent.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
            currentEvent = null;
        }

    }
}
