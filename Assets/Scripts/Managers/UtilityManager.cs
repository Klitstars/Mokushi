using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    [SerializeField] private SOUtilityCard currentEquipment;

    public delegate void onEquipItem();
    public static event onEquipItem OnEquipItem;
    public delegate void onUnequipItem();
    public static event onEquipItem OnUnequipItem;

    public void DrawCard()
    {
        GameManager.instance.HandManager.AddCardToHand(GameManager.instance.DeckManager.DrawRandomUtilityCard());
    }
    public void DrawCards(int cardsToDraw = 1)
    {
        IEnumerable<SOUtilityCard> drawnCards = GameManager.instance.DeckManager.DrawRandomUtilityCards(cardsToDraw);
        GameManager.instance.HandManager.AddCardsToHand(drawnCards);
    }

    public void PlayUtilityCard(SOUtilityCard newUtility, SOEventCard currentEventTarget)
    {
        if (newUtility.UtilityType == Utility.Equipment)
        {
            Equip(newUtility);
            //NEED UI HERE TO SHOW ACTIVE GAME OBJECT
            return;
        }

        newUtility.PlayUtilityCard();
        GameManager.instance.HandManager.RemoveCardFromHand(newUtility);
        GameManager.instance.DeckManager.AddCard(newUtility);
    }

    public void Unequip()
    {
        if (currentEquipment != null)
        {
            GameManager.instance.HandManager.AddCardsToHand((IEnumerable<SOUtilityCard>)currentEquipment);
            RemoveEquipment();

            currentEquipment = null;
        }
    }

    public void RemoveEquipment()
    {
        OnUnequipItem.Invoke();
        currentEquipment = null;
    }

    private void Equip(SOUtilityCard newEquipment)
    {
        Unequip();

        currentEquipment = newEquipment;

        OnEquipItem.Invoke();
    }

    private void SmokescreenRemoval()
    {
        GameManager.instance.keepCurrentEvent = false;
        GameManager.OnStartNewTurn -= SmokescreenRemoval;
    }
}
