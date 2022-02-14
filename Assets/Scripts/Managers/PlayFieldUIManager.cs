using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayFieldUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHealth;
    [SerializeField] private TMP_Text turnCount;
    [SerializeField] private CardUIObject equipmentSlot;

    private List<SOUtilityCard> hand;

    public void UpdatePlayerHealth(int currentHealth)
    {
        playerHealth.text = currentHealth.ToString();
    }

    public void UpdateTurnCount(int currentTurn)
    {
        turnCount.text = currentTurn.ToString();
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

    private void Start()
    {
        hand = new List<SOUtilityCard>();
    }
}
