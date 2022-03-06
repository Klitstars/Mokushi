using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIPlayController : MonoBehaviour
{
    [Header("Text Updates")]
    [SerializeField] private TMP_Text playerHealthPointsText;
    [SerializeField] private TMP_Text turnCountText;
    [SerializeField] private TMP_Text clueCountText;
    [SerializeField] private TMP_Text resultsText;

    [Header("Panel Activations")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject grapplingHookPanel;
    [SerializeField] private GameObject weaponSelectPanel;

    [Header("Button Disables")]
    [SerializeField] private Button endTurnButton;

    [Header("Card UIs")]
    [SerializeField] private GameObject equipmentSlot;
    [SerializeField] private CardUI discardEventCard;

    private List<CardData> hand;
    private CardData grapplingHookDiscardEvent;

    public void UpdatePlayerHealth(int currentHealth)
    {
        playerHealthPointsText.text = currentHealth.ToString();
    }

    public void UpdateTurnCount(int currentTurn)
    {
        turnCountText.text = currentTurn.ToString();
    }

    public void UpdateClueCount(int clueCount)
    {
        clueCountText.text = clueCount.ToString();
    }

    //public int DiscardHand()
    //{
    //    int discardedCardCount = 0;

    //    //Discard hand;

    //    return discardedCardCount;
    //}

    public void AddUICardsToHand(IEnumerable<CardData> cards)
    {
        foreach(CardData card in cards)
        {
            AddUICardToHand(card);
        }
    }

    public void AddUICardToHand(CardData card)
    {
        if (card == null)
            return;


        hand.Add(card);
        GameManager.instance.CardUIBuilder.GenerateUtilityCardUI(card, CardPosition.Hand);
    }

    public void AddUIEventCard(CardData card)
    {
        if (card == null)
            return;

        GameManager.instance.CardUIBuilder.GenerateEventCardUI(card);
    }

    public void RemoveUICardsFromHand(IEnumerable<CardData> cards)
    {
        foreach (CardData card in cards)
        {
            RemoveUICardFromHand(card);
        }
    }

    public void RemoveUICardFromHand(CardData card)
    {
        if (card == null || !hand.Contains(card))
            return;

        Destroy(card.CardUIOjbect);
        hand.Remove(card);
    }

    public void RemoveEventFromStack(CardData card)
    {
        if (card == null)
            return;

        Destroy(card.CardUIOjbect.gameObject);

    }

    public void UpdateEquippedUtility(CardData newEquip)
    {
        GameManager.instance.CardUIBuilder.GenerateUtilityCardUI(newEquip, CardPosition.EquipmentSlot);
    }

    public void UpdateEventCardUI(CardData card, int dangerPoints, int playCount)
    {
        card.CardUIOjbect.GetComponent<CardUI>().UpdateCardUI(card, dangerPoints, playCount);
    }

    public void NullifyEquipment()
    {
        Debug.Log("CardUIPlayController attempted to nullify equipment, but this is not implemented yet.");
        //equipmentSlot.NullifyUI();
    }

    public void GameOver(bool win)
    {
        gameOverPanel.SetActive(true);

        if (win)
            resultsText.text = "You found all four clues!";
        if(!win)
            resultsText.text = "You failed to find all four clues!";
    }

    public void GrapplingHookEventCheck(CardData newEvent, int dangerPoints, int playCount)
    {
        if (grapplingHookDiscardEvent != null)
            grapplingHookDiscardEvent = null;

        grapplingHookDiscardEvent = newEvent;
        grapplingHookPanel.SetActive(true);
        discardEventCard.UpdateCardUI(newEvent, dangerPoints, playCount);
    }

    public void GrapplingHookEventDiscard()
    {
        GameManager.instance.EventManager.GrapplingHookUtilityDiscard(grapplingHookDiscardEvent);
    }

    public void CanEndTurn(bool canEndTurn)
    {
        endTurnButton.interactable = canEndTurn;
        GameManager.instance.CardPlayController.mandatoryCardInPlay = !canEndTurn;
    }

    private void Start()
    {
        hand = new List<CardData>();
    }
}
