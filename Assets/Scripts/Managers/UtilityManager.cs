using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    private DeckController deckController;

    private SOUtilityCard currentEquipment;
    private List<SOUtilityCard> currentAttributes;

    public delegate void onEquipItem();
    public static event onEquipItem OnEquipItem;
    public delegate void onUnequipItem();
    public static event onEquipItem OnUnequipItem;

    public void DrawCard(int cardsToDraw = 1)
    {
        IEnumerable<SOUtilityCard> drawnCards = (IEnumerable<SOUtilityCard>)deckController.DrawRandomCard(cardsToDraw);
        GameManager.instance.HandManagerInstance.AddCardToHand(drawnCards);
    }

    public void PlayUtilityCard(SOUtilityCard newUtility)
    {
        if (newUtility.UtilityType == Utility.Equipment)
        {
            Equip(newUtility);
            return;
        }

        newUtility.UtilityEffect.UtilityEffect();
    }

    public void Unequip()
    {
        if (currentEquipment != null)
        {
            GameManager.instance.HandManagerInstance.AddCardToHand(currentEquipment);
            RemoveEquipment();

            currentEquipment = null;
        }
    }

    public void RemoveEquipment()
    {
        OnUnequipItem.Invoke();
        OnEquipItem -= currentEquipment.OnEquipEffect.OnEquip;
        OnUnequipItem -= currentEquipment.OnUnequipEffect.OnUnequip;

        currentEquipment = null;
    }

    private void Equip(SOUtilityCard newEquipment)
    {
        if (newEquipment.EquipmentType == Equipment.None)
            return;

        Unequip();

        currentEquipment = newEquipment;
        OnEquipItem += currentEquipment.OnEquipEffect.OnEquip;
        OnUnequipItem += currentEquipment.OnUnequipEffect.OnUnequip;

        OnEquipItem.Invoke();
    }

    public void SmokeScreen()
    {
        GameManager.instance.keepCurrentEvent = true;
        GameManager.OnStartNewTurn += SmokescreenRemoval;
    }

    public void WaterVeil() => DrawCard(2);

    public void BloodButterfly()
    {
        DrawCard(2);
        GameManager.instance.EventManagerInstance.UpdateEventDangerModifier(2);
        EventManager.OnEventChanged += BloodButterflyModifierRemoval;
    }
    

    public void Senbonzakura()
    {
        int discardAmount = GameManager.instance.HandManagerInstance.DiscardHand();
        GameManager.instance.EventManagerInstance.DiscardEventCards(discardAmount);
    }

    private void Awake()
    {
        if(deckController == null)
            deckController = GetComponent<DeckController>();
    }

    private void Start() => deckController.RandomizeCardDeck();

    private void BloodButterflyModifierRemoval()
    {
        GameManager.instance.EventManagerInstance.UpdateEventDangerModifier(-2);
        EventManager.OnEventChanged -= BloodButterflyModifierRemoval;
    }

    private void SmokescreenRemoval()
    {
        GameManager.instance.keepCurrentEvent = false;
        GameManager.OnStartNewTurn -= SmokescreenRemoval;
    }

    //Incomplete
    #region Attribute
    public void Precision()
    {
        //+1 Utility point, draw a utility card.
    }

    public void Enduring()
    {
        //+2 Utility points, reduce damage taken by 1 this turn
    }

    public void Devoted()
    {
        //+3 Utility points
    }

    public void Perceptive()
    {
        //+1 utility point, look at the top 3 cards of your event deck, you may order them in any way you like.
    }

    public void Hasty()
    {
        //+5 utility points, draw another event card for this turn. 
    }

    public void Impartial()
    {
        //+3 Utility points, draw a utility card, then draw an event card.
    }

    public void Resentful()
    {
        //+3 Utility points, draw a utility card, then draw an event card.
    }

    public void Detatched()
    {
        //+3 Utility points, draw a utility card, then draw an event card.
    }

    public void Patience()
    {
        //+3 Utility points, draw a utility card, then draw an event card.
    }
    #endregion

    private void UpdateUtilityPoints(int pointsToChange)
    {
        GameManager.instance.EventManagerInstance.UpdateCurrentEventDanger(pointsToChange);
    }
}
