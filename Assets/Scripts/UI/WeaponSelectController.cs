using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectController : MonoBehaviour
{
    [SerializeField] private Button selectWeaponButton;
    
    private List<CardData> weaponCards;
    private CardData selectedWeapon;
    
    public void SelectEquipmentCard(CardData selectedCard)
    {
        if (!weaponCards.Contains(selectedCard))
            return;

        selectedWeapon = selectedCard;
    }

    public void EquipSelectedEquipment()
    {
        if(selectedWeapon != null)
            GameManager.instance.UtilityManager.PlayUtilityCard(selectedWeapon);
    }
     
    private void Start()
    {
        PopulateWeaponSelectUI();
    }

    private void PopulateWeaponSelectUI()
    {
        weaponCards = new List<CardData>();

        foreach (CardData card in GameManager.instance.DeckManager.EquipmentDeck)
        {
            weaponCards.Add(card);
            GameManager.instance.CardUIBuilder.GenerateUtilityCardUI(card, CardPosition.EquipmentSelect);
        }
    }

}
