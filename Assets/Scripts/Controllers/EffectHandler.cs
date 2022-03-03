using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler 
{
    private int currentDurationTimer = 0;
    private bool isEquipment = false;

    private CardEffect thisEffect;

    public delegate void onStatsChanged();
    public static event onStatsChanged OnStatsChanged;

    public EffectHandler(CardEffect newEffect, bool isEquipment)
    {
        thisEffect = newEffect;
        this.isEquipment = isEquipment;
    }

    public void HandleEffect()
    {
        DetermineEffectStartTime();
        DetermineEffectDuration();
    }

    private void DetermineEffectDuration()
    {
        if (isEquipment)
        {
            UtilityManager.OnUnequipItem += CancelEffects;
            return;
        }

        if (thisEffect.EffectDuration >= 1)
            GameManager.OnStartNewTurn += CancelEffects;
    }

    private void DetermineEffectStartTime()
    {
        if (thisEffect.EffectTiming == EffectTiming.Immediate)
            DetermineEffect();
        if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnStart)
            GameManager.OnStartNewTurn += DetermineEffect;
        if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnEnd)
            GameManager.OnEndTurn += DetermineEffect;
    }

    private void CancelEffects()
    {
        DetermineCancelation();

        if (isEquipment)
            UtilityManager.OnUnequipItem -= CancelEffects;
    }

    private void DetermineEffect()
    {
        switch (thisEffect.effectType)
        {
            case EffectTypes.DrawUtility:
                DrawUtility(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.DrawEvent:
                DrawEvent(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.ReorderEventDeck:
                ReorderEvents(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.ReorderUtilityDeck:
                ReorderUtility(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.DangerPointModifier:
                DangerPointModifier(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.PlayCountModifier:
                PlayCountModifier(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.DamageTakenModifier:
                ModifyDamageTaken(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.NullifyEventDamage:
                NullifyEventDamage(true);
                return;

            case EffectTypes.CycleEventDraw:
                CycleEventDraw(true, thisEffect.EffectMagnitude);
                return;

            case EffectTypes.UtilityDrawModifier:
                UtilityDrawModifier(thisEffect.EffectMagnitude);
                return;

            case EffectTypes.EventDrawModifier:
                EventDrawModifier(thisEffect.EffectMagnitude);
                return;
            case EffectTypes.BrokenKatana:
                BrokenKatana(true);
                return;
            case EffectTypes.GrapplingHook:
                GrapplingHook(true);
                return;
        }


        if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnStart)
            GameManager.OnStartNewTurn -= DetermineEffect;
        if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnEnd)
            GameManager.OnEndTurn -= DetermineEffect;
    }

    private void DetermineCancelation()
    {
        switch (thisEffect.effectType)
        {
            case EffectTypes.ReorderEventDeck:
                ReorderEvents(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.ReorderUtilityDeck:
                ReorderUtility(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.DangerPointModifier:
                DangerPointModifier(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.PlayCountModifier:
                PlayCountModifier(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.DamageTakenModifier:
                ModifyDamageTaken(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.NullifyEventDamage:
                NullifyEventDamage(false);
                return;

            case EffectTypes.CycleEventDraw:
                CycleEventDraw(false, -thisEffect.EffectMagnitude);
                return;

            case EffectTypes.UtilityDrawModifier:
                UtilityDrawModifier(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.EventDrawModifier:
                EventDrawModifier(-thisEffect.EffectMagnitude);
                return;

            case EffectTypes.BrokenKatana:
                BrokenKatana(false);
                return;
            case EffectTypes.GrapplingHook:
                GrapplingHook(false);
                return;
        }
    }

    private void CancelTimer()
    {
        currentDurationTimer++;

        if (currentDurationTimer > thisEffect.EffectDuration)
        {
            CancelEffects();

            if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnStart)
                GameManager.OnStartNewTurn -= DetermineEffect;
            if (thisEffect.EffectTiming == EffectTiming.EnactAtTurnEnd)
                GameManager.OnEndTurn -= DetermineEffect;
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

    private void CycleEventDraw(bool hasGrapplingHook, int effectMagnitude)
    {
        GameManager.instance.EventManager.hasGrapplingHook = hasGrapplingHook;
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

    private void BrokenKatana(bool hasBrokenKatana)
    {
        GameManager.instance.EventManager.hasBrokenKatana = hasBrokenKatana;
    }

    private void GrapplingHook(bool hasGrapplingHook)
    {
        GameManager.instance.EventManager.hasGrapplingHook = hasGrapplingHook;
    }
}
