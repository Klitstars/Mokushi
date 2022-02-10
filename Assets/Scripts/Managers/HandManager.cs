using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private List<SOCardBase> hand;
    private DeckController deckController;

    public int DiscardHand()
    {
        int discardedCardCount = 0;

        //Discard hand;

        return discardedCardCount;
    }

    public void AddCardToHand(IEnumerable<SOCardBase> cards)
    {
        foreach(SOCardBase card in cards)
            hand.Add(card);
    }
}
