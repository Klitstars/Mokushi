using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEventCard", menuName = "Cards/NewEventCard", order = 1)]
public class SOEventCard : SOCardBase
{
    [Header("Event Attributes")]
    [SerializeField] private int dangerPoints;
    [SerializeField] private int playNumber;
    [SerializeField] private EventCardType eventType;

    public EventCardType EventType { get => eventType; }

}
