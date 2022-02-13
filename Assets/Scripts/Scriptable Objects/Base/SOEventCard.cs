using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEventCard", menuName = "Cards/NewEventCard", order = 1)]
public class SOEventCard : SOCardBase
{
    [Header("Event Attributes")]
    [SerializeField] private int maxDangerPoints;
    [SerializeField] private int maxPlayNumber;
    [SerializeField] private EventCardType eventType;
    [SerializeField] private SOEventEffect eventEffect;

    private int currentDangerPoints;
    private int currentPlayNumber;

    public EventCardType EventType { get => eventType; }
    public SOEventEffect EventEffect { get => eventEffect; }
    public int MaxDangerPoints { get => maxDangerPoints; }
    public int MaxPlayNumber { get => maxPlayNumber; }
    public int CurrentDangerPoints { get => currentDangerPoints; }
    public int CurrentPlayNumber { get => currentPlayNumber; }

    public void UpdateDangerPoints(int pointsToChange) => currentDangerPoints += pointsToChange;
    public void UpdatePlayCount(int pointsToChange) => currentPlayNumber += pointsToChange;

    public void OnEventStarted()
    {
        currentDangerPoints = 0;
        currentPlayNumber = 0;
    }

    public void OnEventEnded()
    {

    }
}
