﻿using System.Collections;
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
    [SerializeField] private bool enactAtStart;
    [SerializeField] private bool enactAtEnd;

    private int currentDangerPoints;
    private int currentPlayNumber;
    private int eventDangerModifier = 0;
    private int eventPlayCountModifier = 0;

    public EventCardType EventType { get => eventType; }
    public SOEventEffect EventEffect { get => eventEffect; }
    public int MaxDangerPoints { get => maxDangerPoints; }
    public int MaxPlayNumber { get => maxPlayNumber; }
    public int CurrentDangerPoints { get => currentDangerPoints; set => currentDangerPoints = value; }
    public int CurrentPlayNumber { get => currentPlayNumber; set => currentPlayNumber = value; }

    public void UpdateDangerPoints(int pointsToChange) => currentDangerPoints -= pointsToChange;
    public void UpdatePlayCount(int pointsToChange) => currentPlayNumber -= pointsToChange;

    public void OnEventStarted()
    {
        currentDangerPoints = maxDangerPoints;
        currentPlayNumber = maxPlayNumber;

        if (enactAtStart)
            EventEffect.InitiateEffect();
    }

    public void OnEventEnded()
    {
        if (enactAtEnd)
            EventEffect.InitiateEffect();
    }
}
