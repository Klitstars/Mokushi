using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIBuilder : MonoBehaviour
{
    [Header("Card Prefabs")]
    [SerializeField] private GameObject cardPrefab;

    [Header("Card Positions")]
    [SerializeField] private Transform handPosition;
    [SerializeField] private Transform equipmentPosition;
    [SerializeField] private Transform eventPosition;

    public void GenerateUtilityCard(CardData newCard, UtilityPosition position)
    {
        if (position == UtilityPosition.None)
            return;

        GameObject newCardUI = null;

        if(position == UtilityPosition.Hand)
            newCardUI = Instantiate(cardPrefab, handPosition.transform);
        else if(position == UtilityPosition.EquipmentSlot)
            newCardUI = Instantiate(cardPrefab, equipmentPosition.transform);

        if(newCardUI == null)
        {
            Debug.Log("Oops, no Card UI was found in the Card Builder.");
            return;
        }

        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardUI>().InitUtilityCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;
    }

    public GameObject GenerateEventCard(CardData newCard)
    {
        GameObject newCardUI;
        newCardUI = Instantiate(cardPrefab, eventPosition.transform);

        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardUI>().InitEventCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;


        return newCardUI;
    }
}
