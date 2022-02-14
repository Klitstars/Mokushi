using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayFieldUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text turnCountText;
    [SerializeField] private TMP_Text clueCountText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject grapplingHookPanel;
    [SerializeField] private CardUIObject equipmentSlot;
    [SerializeField] private CardUIObject discardEventCard;

    private List<SOUtilityCard> hand;
    private SOEventCard grapplingHookDiscardEvent;

    public void UpdatePlayerHealth(int currentHealth)
    {
        playerHealthText.text = currentHealth.ToString();
    }

    public void UpdateTurnCount(int currentTurn)
    {
        turnCountText.text = currentTurn.ToString();
    }

    public void UpdateClueCount(int clueCount)
    {
        clueCountText.text = clueCount.ToString();
    }

    public int DiscardHand()
    {
        int discardedCardCount = 0;

        //Discard hand;

        return discardedCardCount;
    }

    public void AddCardsToHand(IEnumerable<SOUtilityCard> cards)
    {
        foreach(SOUtilityCard card in cards)
        {
            AddCardToHand(card);
        }
    }

    public void AddCardToHand(SOUtilityCard card)
    {
        if (card == null)
            return;

        hand.Add(card);
        GameManager.instance.CardBuilder.GenerateCard(card, UtilityPosition.Hand);
    }

    public void RemoveCardFromHand(IEnumerable<SOUtilityCard> cards)
    {
        foreach (SOUtilityCard card in cards)
        {
            RemoveCardFromHand(card);
        }
    }

    public void RemoveCardFromHand(SOUtilityCard card)
    {
        if (card == null || !hand.Contains(card))
            return;

        Destroy(card.CardUIOjbect);
        hand.Remove(card);
    }

    public void UpdateEquippedUtility(SOUtilityCard newEquip)
    {
        equipmentSlot.UpdateEquipmentUI(newEquip);
    }

    public void SelectEquipmentSlot(bool isSelected)
    {
        equipmentSlot.isPickedUp = isSelected;
    }

    public void UpdateEventCardUI(SOEventCard card, int dangerPoints, int playCount)
    {
        card.CardUIOjbect.GetComponent<CardUIObject>().UpdateCardUI(card, dangerPoints, playCount);
    }

    public void NullifyEquipment()
    {
        equipmentSlot.NullifyUI();
    }

    public void GameOver(bool win)
    {
        gameOverPanel.SetActive(true);
    }

    public void GrapplingHookEventCheck(SOEventCard newEvent, int dangerPoints, int playCount)
    {
        Debug.Log("Grappling hook check.");
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

    private void Start()
    {
        hand = new List<SOUtilityCard>();
    }
}
