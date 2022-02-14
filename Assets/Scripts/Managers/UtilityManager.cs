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

    public SOUtilityCard EquippedUtility { get => currentEquipment; }

    public void DrawCard()
    {
        GameManager.instance.PlayFieldUIManager.AddCardToHand(GameManager.instance.DeckManager.DrawRandomUtilityCard());
    }
    public void DrawCards(int cardsToDraw = 1)
    {
        IEnumerable<SOUtilityCard> drawnCards = GameManager.instance.DeckManager.DrawRandomUtilityCards(cardsToDraw);
        GameManager.instance.PlayFieldUIManager.AddCardsToHand(drawnCards);
    }

    public void PlayUtilityCard(SOUtilityCard newUtility)
    {
        if (newUtility.UtilityType == Utility.Equipment)
        {
            Equip(newUtility);
            //NEED UI HERE TO SHOW ACTIVE GAME OBJECT
            return;
        }

        newUtility.PlayUtilityCard();
        GameManager.instance.PlayFieldUIManager.RemoveCardFromHand(newUtility);
        GameManager.instance.DeckManager.AddCard(newUtility);
    }

    public void Unequip()
    {
        if (currentEquipment != null)
        {
            GameManager.instance.PlayFieldUIManager.AddCardToHand(currentEquipment);
            RemoveEquipment();
        }
    }

    public void DestroyEquipment()
    {
        if (currentEquipment == null)
            return;

        Destroy(currentEquipment.CardUIOjbect);
        RemoveEquipment();
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
        GameManager.instance.PlayFieldUIManager.UpdateEquippedUtility(newEquipment);
        newEquipment.PlayUtilityCard();

        OnEquipItem.Invoke();
        GameManager.instance.PlayFieldUIManager.RemoveCardFromHand(newEquipment);
    }

    private void SmokescreenRemoval()
    {
        GameManager.instance.keepCurrentEvent = false;
        GameManager.OnStartNewTurn -= SmokescreenRemoval;
    }
}
