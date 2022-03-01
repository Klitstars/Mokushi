using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIBuilder : MonoBehaviour
{
    [Header("Card Prefabs")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject weaponCardPrefab;

    [Header("Card Positions")]
    [SerializeField] private Transform handPosition;
    [SerializeField] private Transform equipmentPosition;
    [SerializeField] private Transform eventPosition;
    [SerializeField] private Transform equipmentSelectPosition;

    public void GenerateUtilityCardUI(CardData newCard, CardPosition position)
    {
        if (position == CardPosition.None)
            return;

        GameObject newCardUI = null;

        if (position == CardPosition.Hand)
            newCardUI = Instantiate(cardPrefab, handPosition.transform);
        else if (position == CardPosition.EquipmentSlot)
            newCardUI = Instantiate(cardPrefab, equipmentPosition.transform);
        else if (position == CardPosition.EquipmentSelect)
            newCardUI = Instantiate(weaponCardPrefab, equipmentSelectPosition.transform);

        if(newCardUI == null)
        {
            Debug.Log("Oops, no Card UI was found in the Card Builder.");
            return;
        }

        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardUI>().InitUtilityCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;
    }

    public GameObject GenerateEventCardUI(CardData newCard)
    {
        GameObject newCardUI;
        newCardUI = Instantiate(cardPrefab, eventPosition.transform);

        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardUI>().InitEventCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;


        return newCardUI;
    }
}
