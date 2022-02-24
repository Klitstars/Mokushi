using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
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


    private CardData utilityCardData;
    private CardData eventCardData;
    private bool isHighlighted = false;

    public bool isPickedUp = false;

    public CardData UtilityCardData { get => utilityCardData; }
    public CardData EventCardData { get => eventCardData; }


    public void InitUtilityCardUI(CardData newCard)
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

    public void UpdateEquipmentUI(CardData newCard)
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

    public void UpdateCardUI(CardData newCard, int dangerPoints, int playCount)
    {
        if (newCard == null)
            return;

        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerPointsText.text = dangerPoints.ToString();
        playCountPointsText.text = playCount.ToString();

        if (newCard.CardEffects.Select(x => x.effectType).Contains(EffectTypes.Clue))
        {
            dangerText.text = "";
            dangerPointsText.text = "";
            playCountText.text = "";
            playCountPointsText.text = "";
        }

        eventCardData = newCard;
    }

    public void InitEventCardUI(CardData newCard)
    {
        if (newCard == null)
            return; 

        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;
        dangerPointsText.text = newCard.CurrentDangerPoints.ToString();
        playCountPointsText.text = newCard.CurrentPlayNumber.ToString();

        if (newCard.CardEffects.Select(x => x.effectType).Contains(EffectTypes.Clue))
        {
            dangerText.text = "";
            dangerPointsText.text = "";
            playCountText.text = "";
            playCountPointsText.text = "";
        }        

        eventCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void SelectCard()
    {
        if(utilityCardData != null)
        {
            GameManager.instance.CardPlayController.UpdateSelectedUtility(UtilityCardData);
            return;
        }

        if(eventCardData != null)
        {
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
        if (utilityCardData == null)
            return;

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

        if (utilityCardData.IsMandatory && highlightImage.color != Color.red)
            highlightImage.color = Color.red;
    }
}
