using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image cardForeground;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private Transform homePoint;
    [SerializeField] private float lerpSpeed;

    private SOUtilityCard utilityCardData;
    private SOEventCard eventCardData;
    public bool isPickedUp = false;

    public SOUtilityCard UtilityCardData { get => utilityCardData; }
    public SOEventCard EventCardData { get => eventCardData; }


    public void UpdateCardUI(SOUtilityCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;

        utilityCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void UpdateCardUI(SOEventCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;

        eventCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void SelectCard()
    {
        if(utilityCardData != null)
        {
            Debug.Log("Utility selected: " + utilityCardData.name);
            GameManager.instance.CardPlayController.UpdateCurrentUtility(UtilityCardData);
            return;
        }
        if(eventCardData != null)
        {
            Debug.Log("Event selected: " + eventCardData.name);
            GameManager.instance.CardPlayController.UpdateCurrentEvent(EventCardData);
            return;
        }
    }

    private void Update()
    {
        
    }
}
