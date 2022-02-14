using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityEffect", menuName = "Effects/NewUtilityEffect", order = 1)]
public class SOUtilityEffect : ScriptableObject
{
    [Header("Base Effect Attributes")]
    [SerializeField] private UtilityEffectTypes effectType;
    [SerializeField] private int effectMagnitude;


    [Header("Effects With A Turn Duration")]
    [SerializeField] private int effectDuration;

    public delegate void onStatsChanged();
    public static event onStatsChanged OnStatsChanged;

    private int currentDurationTimer = 0;

    public void InitiateEffect()
    {
        DetermineEffect(effectMagnitude);

        if (effectDuration > 0)
            GameManager.OnStartNewTurn += CancelTimer;
    }

    public void CancelEffects()
    {
        Debug.Log("Cancelling effects");
        DetermineCancelation(-effectMagnitude);
    }

    private void DetermineEffect(int effectMagnitude)
    {
        switch(effectType)
        {
            case UtilityEffectTypes.DrawUtility:
                DrawUtility(effectMagnitude);
                return;

            case UtilityEffectTypes.DrawEvent:
                DrawEvent(effectMagnitude);
                return;


            case UtilityEffectTypes.ReorderEventDeck:
                ReorderEvents(effectMagnitude);
                return;

            case UtilityEffectTypes.ReorderUtilityDeck:
                ReorderUtility(effectMagnitude);
                return;

            case UtilityEffectTypes.DangerPointModifier:
                DangerPointModifier(effectMagnitude);
                return;

            case UtilityEffectTypes.PlayCountModifier:
                PlayCountModifier(effectMagnitude);
                return;

            case UtilityEffectTypes.DamageTakenModifier:
                ModifyDamageTaken(effectMagnitude);
                return;

            case UtilityEffectTypes.NullifyEventDamage:
                NullifyEventDamage(true);
                return;

            case UtilityEffectTypes.MandatoryPlay:
                MandatoryPlay();
                return;

            case UtilityEffectTypes.CycleEventDraw:
                CycleEventDraw(true);
                return;

            case UtilityEffectTypes.UtilityDrawModifier:
                UtilityDrawModifier(effectMagnitude);
                return;

            case UtilityEffectTypes.EventDrawModifier:
                EventDrawModifier(effectMagnitude);
                return;
        }
    }

    private void DetermineCancelation(int effectMagnitude)
    {
        switch (effectType)
        {
            case UtilityEffectTypes.ReorderEventDeck:
                ReorderEvents(effectMagnitude);
                return;

            case UtilityEffectTypes.ReorderUtilityDeck:
                ReorderUtility(effectMagnitude);
                return;

            case UtilityEffectTypes.DangerPointModifier:
                DangerPointModifier(effectMagnitude);
                return;

            case UtilityEffectTypes.PlayCountModifier:
                PlayCountModifier(effectMagnitude);
                return;

            case UtilityEffectTypes.DamageTakenModifier:
                ModifyDamageTaken(effectMagnitude);
                return;

            case UtilityEffectTypes.NullifyEventDamage:
                NullifyEventDamage(false);
                return;

            case UtilityEffectTypes.MandatoryPlay:
                MandatoryPlay();
                return;

            case UtilityEffectTypes.CycleEventDraw:
                CycleEventDraw(false);
                return;

            case UtilityEffectTypes.UtilityDrawModifier:
                return;

            case UtilityEffectTypes.EventDrawModifier:
                return;
        }
    }

    private void CancelTimer()
    {
        currentDurationTimer++;

        if (currentDurationTimer >= effectDuration)
        {
            CancelEffects();
            GameManager.OnStartNewTurn -= CancelTimer;
        }
    }

    private void DrawUtility(int magnitude)
    {
        GameManager.instance.UtilityManager.DrawCards(magnitude);
    }

    private void DrawEvent(int magnitude)
    {
        GameManager.instance.EventManager.DrawCardsAndUpdateEvents(magnitude);
    }

    private void ReorderUtility(int magnitude)
    {
        //UI component that lets players reorder the upcoming draws.
    }

    private void ReorderEvents(int magnitude)
    {
        //UI component that lets players reorder the upcoming draws.

    }

    private void DangerPointModifier(int magnitude)
    {
        GameManager.instance.EventManager.UpdateEventDangerModifier(magnitude);
        OnStatsChanged.Invoke();
    }

    private void PlayCountModifier(int magnitude)
    {
        GameManager.instance.EventManager.UpdateEventPlayCountModifier(magnitude);
        OnStatsChanged.Invoke();
    }

    private void NullifyEventDamage(bool isDamageNullified)
    {
        GameManager.instance.nullifyDamage = isDamageNullified;
    }

    private void CycleEventDraw(bool hasGrapplingHook)
    {
        GameManager.instance.EventManager.hasGrapplingHook = hasGrapplingHook;
    }

    private void MandatoryPlay()
    {
        //UI component that disallows ending the turn until this card is played.
    }

    private void ModifyDamageTaken(int effectMagnitude)
    {
        GameManager.instance.UpdateDamageModifier(effectMagnitude);
    }

    private void UtilityDrawModifier(int effectMagnitude)
    {
        GameManager.instance.UpdateDrawModifier(effectMagnitude);
    }

    private void EventDrawModifier(int effectMagnitude)
    {
        GameManager.instance.UpdateDrawModifier(effectMagnitude);
    }
}
