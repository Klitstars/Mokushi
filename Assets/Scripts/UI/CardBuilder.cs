using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardBuilder : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handPosition;
    [SerializeField] private Transform eventPosition;

    public GameObject GenerateCard(SOUtilityCard newCard)
    {
        GameObject newCardUI;
        newCardUI = Instantiate(cardPrefab, handPosition.transform);


        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardObject>().UpdateCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;

        return newCardUI;
    }

    public GameObject GenerateCard(SOEventCard newCard)
    {
        GameObject newCardUI;
        newCardUI = Instantiate(cardPrefab, eventPosition.transform);

        newCardUI.gameObject.SetActive(true);
        newCardUI.GetComponent<CardObject>().UpdateCardUI(newCard);
        newCard.CardUIOjbect = newCardUI;


        return newCardUI;
    }
}
