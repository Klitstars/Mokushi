using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField] private DeckObject utilityDeck;
    [SerializeField] private DeckObject eventDeck;

#if(UNITY_EDITOR)
    [Header("Debug")]
    [SerializeField] private CardType deckToTest;
    [SerializeField] private int numberToDraw;
    public void GetRandomCardTest()
    {
        GetRandomCard(deckToTest, 1);
    }
#endif

    [ContextMenu("DrawCards")]
    public SOCardBase GetRandomCard(CardType deckType, int cardsToDraw = 1)
    {
        List<SOCardBase> drawnCards = new List<SOCardBase>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            int randomInt = Random.Range(0, GetCorrectDeck(deckType).CardDeck.Count);
            drawnCards.Add(GetCorrectDeck(deckType).CardDeck[randomInt]);
        }

        AnnounceDrawEditor(drawnCards);
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetRandomCardTest();
    }

    private DeckObject GetCorrectDeck(CardType deckType)
    {
        if (deckType == CardType.Event)
            return eventDeck;
        else
            return utilityDeck;
    }

    private void AnnounceDrawEditor(IEnumerable<SOCardBase> cards)
    {
        foreach(SOCardBase card in cards)
        {
            Debug.Log(card.name);
        }
    }
}
