using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect 
{
    [SerializeField] private EffectTypes cardEffect;
    [SerializeField] private Equipment effectTarget;
    [SerializeField] private int effectMagnitude;
    [SerializeField] private int effectDuration;
    [SerializeField] private bool enactAtStartOfTurn;
    [SerializeField] private bool enactAtEndOfTurn;

    public EffectTypes effectType { get => cardEffect; }
    public Equipment EffectTarget { get => effectTarget; }
    public int EffectMagnitude { get => effectMagnitude; }
    public int EffectDuration { get => effectDuration; }
    public bool EnactAtStartOfTurn { get => enactAtStartOfTurn; }
    public bool EnactAtEndOfTurn { get => enactAtEndOfTurn; }
}
