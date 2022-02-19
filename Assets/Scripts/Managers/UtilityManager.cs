using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    [SerializeField] private CardData currentEquipment;

    public delegate void onEquipItem();
    public static event onEquipItem OnEquipItem;
    public delegate void onUnequipItem();
    public static event onEquipItem OnUnequipItem;

    public CardData CurrentEquipment { get => currentEquipment; }

    public void DrawCard()
    {
        CardData newCard = GameManager.instance.DeckManager.DrawRandomUtilityCard();

        if (newCard == null)
            return;

        if(newCard.IsMandatory)
        {
            GameManager.instance.CanEndTurn(-1);
            if (GameManager.instance.EventManager.CurrentEventCards.Count == 0)
                GameManager.instance.EventManager.DrawCardAndUpdateEvents();
        }

        GameManager.instance.PlayFieldUIManager.AddCardToHand(newCard);
    }

    public void DrawCards(int cardsToDraw = 1)
    {
        for (int i = 0; i < cardsToDraw; i++)
            DrawCard();
    }

    public void PlayUtilityCard(CardData newUtility)
    {
        if (newUtility.UtilityType == UtilityType.Equipment)
        {
            Equip(newUtility);
            //NEED UI HERE TO SHOW ACTIVE GAME OBJECT
            return;
        }

        newUtility.PlayUtilityCard();
        GameManager.instance.PlayFieldUIManager.RemoveCardFromHand(newUtility);
        GameManager.instance.DeckManager.AddCardToUtilityDeck(newUtility);
    }

    public void Unequip()
    {
        if (currentEquipment != null)
        {
            GameManager.instance.PlayFieldUIManager.AddCardToHand(currentEquipment);
            RemoveEquipment();
        }
    }

    public void DestroyEquippedItem()
    {
        if (currentEquipment == null)
            return;

        GameManager.instance.PlayFieldUIManager.NullifyEquipment();
        RemoveEquipment();
    }

    private void RemoveEquipment()
    {
        OnUnequipItem.Invoke();
        currentEquipment = null;
    }

    private void Equip(CardData newEquipment)
    {
        Unequip();

        currentEquipment = newEquipment;
        GameManager.instance.PlayFieldUIManager.UpdateEquippedUtility(newEquipment);
        newEquipment.PlayUtilityCard();

        OnEquipItem.Invoke();
        GameManager.instance.PlayFieldUIManager.RemoveCardFromHand(newEquipment);
    }
}
