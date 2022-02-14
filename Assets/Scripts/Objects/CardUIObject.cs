using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIObject : MonoBehaviour
{
    [Header("Card Attributes")]
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image cardForeground;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text dangerText;
    [SerializeField] private TMP_Text dangerPointsText;
    [SerializeField] private TMP_Text playCountText;
    [SerializeField] private TMP_Text playCountPointsText;
    [SerializeField] private Image highlightImage;
    [SerializeField] private Transform homePoint;
    [SerializeField] private float lerpSpeed;

    private SOUtilityCard utilityCardData;
    private SOEventCard eventCardData;
    private bool isHighlighted = false;

    public bool isPickedUp = false;

    public SOUtilityCard UtilityCardData { get => utilityCardData; }
    public SOEventCard EventCardData { get => eventCardData; }


    public void InitCardUI(SOUtilityCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerText.text = "Utility Points";
        dangerPointsText.text = newCard.UtilityPoints.ToString();
        playCountText.text = "";
        playCountPointsText.text = "";


        utilityCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void UpdateEquipmentUI(SOUtilityCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerText.text = "Utility Points";
        dangerPointsText.text = newCard.UtilityPoints.ToString();
        playCountText.text = "";
        playCountPointsText.text = "";


        utilityCardData = newCard;
    }

    public void NullifyUI()
    {
        cardBackground.sprite = null;
        cardForeground.sprite = null;
        cardName.text = "";
        cardDescription.text = "";
        dangerText.text = "";
        dangerPointsText.text = "";
        playCountText.text = "";
        playCountPointsText.text = "";

        utilityCardData = null;
    }

    public void UpdateCardUI(SOEventCard newCard, int dangerPoints, int playCount)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerPointsText.text = dangerPoints.ToString();
        playCountPointsText.text = playCount.ToString();

        eventCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void InitCardUI(SOEventCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerPointsText.text = newCard.CurrentDangerPoints.ToString();
        playCountPointsText.text = newCard.CurrentPlayNumber.ToString();

        eventCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void SelectCard()
    {
        if(utilityCardData != null)
        {
            Debug.Log("Utility selected: " + utilityCardData.name);
            GameManager.instance.CardPlayController.UpdateSelectedUtility(UtilityCardData);
            return;
        }
        if(eventCardData != null)
        {
            Debug.Log("Event selected: " + eventCardData.name);
            GameManager.instance.CardPlayController.UpdateSelectedEvent(EventCardData);
            return;
        }
    }

    public void SelectEquipment()
    {
        GameManager.instance.CardPlayController.SelectEquipmentSlot();
    }

    private void Update()
    {
        Highlight();
    }

    private void Highlight()
    {
        if (isPickedUp && !isHighlighted)
        {
            highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 255);
            isHighlighted = true;
        }

        if(!isPickedUp && isHighlighted)
        {
            highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, 0);
            isHighlighted = false;
        }
    }
}
