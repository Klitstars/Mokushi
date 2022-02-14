using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayController : MonoBehaviour
{
    private SOEventCard currentEvent;
    private SOUtilityCard currentUtility;
    public bool equipmentSlotSelected = false;

    public void UpdateSelectedEvent(SOEventCard card)
    {
        if (equipmentSlotSelected)
            equipmentSlotSelected = false;

        if(currentEvent != null)
            currentEvent.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = false;

        if (currentEvent == card)
        {
            currentEvent = null;
            return;
        }

        currentEvent = card;
        card.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = true;

        if (currentEvent.EventType == EventCardType.Clue)
        {
            GameManager.instance.EventManager.RemoveClue(card);
            currentEvent = null;
        }

        PlayCards();
    }

    public void UpdateSelectedUtility(SOUtilityCard card)
    {
        if(currentUtility != null)
            currentUtility.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = false;

        if(currentUtility == card)
        {
            currentUtility = null;
            return;
        }

        currentUtility = card;
        card.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = true;

        PlayCards();
    }

    public void SelectEquipmentSlot()
    {
        if(equipmentSlotSelected)
        {
            equipmentSlotSelected = false;
            GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);
            return;
        }

        equipmentSlotSelected = true;
        GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);

        if(currentEvent != null)
        {
            currentEvent.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = false;
            currentEvent = null;
        }

        PlayCards();
    }

    private void PlayCards()
    {
        if (currentUtility == null)
            return;

        if(currentUtility.EquipmentType == Equipment.None && currentEvent != null && currentEvent.EventType != EventCardType.Clue)
        {
            if (GameManager.instance.EventManager.PlayUtilityOnEvent(currentUtility, currentEvent))
                GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility);

            currentUtility.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = false;
            currentEvent.CardUIOjbect.GetComponent<CardUIObject>().isPickedUp = false;

            currentUtility = null;
            currentEvent = null;
            return;
        }

        if(currentUtility.EquipmentType != Equipment.None && equipmentSlotSelected)
        {
            GameManager.instance.PlayFieldUIManager.UpdateEquippedUtility(currentUtility);
            GameManager.instance.UtilityManager.PlayUtilityCard(currentUtility);

            currentUtility = null;
            equipmentSlotSelected = false;
            GameManager.instance.PlayFieldUIManager.SelectEquipmentSlot(equipmentSlotSelected);

            return;
        }
    }
}
