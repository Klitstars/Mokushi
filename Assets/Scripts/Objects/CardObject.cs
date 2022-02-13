using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image cardForeground;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private Transform homePoint;
    [SerializeField] private float lerpSpeed;

    private SOUtilityCard utilityCardData;
    private SOEventCard eventCardData;
    private bool isPickedUp = false;

    public SOUtilityCard UtilityCardData { get => utilityCardData; }
    public SOEventCard EventCardData { get => eventCardData; }


    public void UpdateCardUI(SOUtilityCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;

        utilityCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void UpdateCardUI(SOEventCard newCard)
    {
        cardBackground.sprite = newCard.CardBackground;
        cardForeground.sprite = newCard.CardForeground;
        cardName.text = newCard.CardName;
        cardDescription.text = newCard.CardDescription;

        eventCardData = newCard;
        newCard.CardUIOjbect = this.gameObject;
    }

    public void SelectCard()
    {
        if(utilityCardData != null)
        {
            Debug.Log("Utility selected: " + utilityCardData.name);
            GameManager.instance.CardPlayController.UpdateCurrentUtility(UtilityCardData);
            return;
        }
        if(eventCardData != null)
        {
            Debug.Log("Event selected: " + eventCardData.name);
            GameManager.instance.CardPlayController.UpdateCurrentEvent(EventCardData);
            return;
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        //    if (hit.collider != null)
        //    {
        //        Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
        //        CardObject targetCard = hit.collider.GetComponent<CardObject>();
        //        targetCard.GetComponentInParent<Transform>().DetachChildren();
        //        targetCard.isPickedUp = !targetCard.isPickedUp;
        //    }
        //}

        //ReturnHome();
    }

    private void ReturnHome()
    {
        if (transform.position != homePoint.position && !isPickedUp)
            transform.position = Vector3.Lerp(transform.position, new Vector3(homePoint.position.x, homePoint.position.y, transform.position.z), lerpSpeed * Time.deltaTime);
    }
}
