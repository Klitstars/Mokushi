using System.Collections;
using System.Collections.Generic;
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

        if (currentEvent.EventType == EventCardType.Clue)
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

    public void SelectEquipmentSlot()
    {
        if (mandatoryCardInPlay)
            return;

        if (currentUtility == null && GameManager.instance.UtilityManager.CurrentEquipment != null)
        {
            GameManager.instance.UtilityManager.Unequip();
            GameManager.instance.PlayFieldUIManager.NullifyEquipment();
            equipmentSlotSelected = false;
            GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);

            return;
        }

        if (currentUtility == null && GameManager.instance.UtilityManager.CurrentEquipment == null)
            return;

        if (equipmentSlotSelected)
        {
            equipmentSlotSelected = false;
            GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);
            
            return;
        }

        equipmentSlotSelected = true;
        GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);

        if(currentEvent != null)
        {
            currentEvent.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
            currentEvent = null;
        }

        PlayCards();
    }

    private void PlayCards()
    {
        if (currentUtility == null)
            return;

        if (currentUtility.EquipmentType != Equipment.None && equipmentSlotSelected)
        {
            GameManager.instance.PlayFieldUIManager.UpdateEquippedUtility(currentUtility);
            GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility);

            currentUtility.CardUIOjbect.GetComponent<CardUI>().isPickedUp = false;
            currentUtility = null;

            equipmentSlotSelected = false;
            GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);

            return;
        }

        if (currentEvent != null)
        {
            if (GameManager.instance.EventManager.PlayUtilityOnEvent(currentUtility, currentEvent))
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
