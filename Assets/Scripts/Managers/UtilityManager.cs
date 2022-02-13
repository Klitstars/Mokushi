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

    public void DrawCard(int cardsToDraw = 1)
    {
        IEnumerable<SOUtilityCard> drawnCards = GameManager.instance.DeckManager.DrawRandomUtilityCard(cardsToDraw);
        GameManager.instance.HandManager.AddCardToHand(drawnCards);
    }

    public void PlayUtilityCard(SOUtilityCard newUtility, SOEventCard currentEventTarget)
    {
        if (newUtility.UtilityType == Utility.Equipment)
        {
            Equip(newUtility);
            return;
        }
        
        if (!GameManager.instance.EventManager.CurrentEventCards.Contains(currentEventTarget))
            return;

        Debug.Log("Attempting to play utility card.");
        if(GameManager.instance.EventManager.UpdateEventDangerPoints(newUtility, currentEventTarget))
            newUtility.PlayUtilityCard();
    }

    public void Unequip()
    {
        if (currentEquipment != null)
        {
            GameManager.instance.HandManager.AddCardToHand((IEnumerable<SOUtilityCard>)currentEquipment);
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
