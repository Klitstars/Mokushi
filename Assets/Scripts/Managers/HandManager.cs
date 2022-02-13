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

    public void AddCardToHand(IEnumerable<SOUtilityCard> cards)
    {
        foreach(SOUtilityCard card in cards)
        {
            if (card == null)
                return;

            hand.Add(card);
            GameManager.instance.CardBuilder.GenerateCard(card);
        }
    }

    private void Start()
    {
        hand = new List<SOUtilityCard>();
    }
}
