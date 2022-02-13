using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<SOUtilityCard> hand;


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
        GameManager.instance.CardBuilder.GenerateCard(card);
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
        if (card == null)
            return;

        Destroy(card.CardUIOjbect);
        hand.Remove(card);
    }

    private void Start()
    {
        hand = new List<SOUtilityCard>();
    }
}
