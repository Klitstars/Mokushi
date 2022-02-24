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

    public EffectHandler(CardEffect newEffect)
    {
        thisEffect = newEffect;
    }

    public void HandleEffect()
    {
        DetermineEffect(thisEffect);

        if (thisEffect.EffectDuration == 0)
        {
            isEquipment = true;
            UtilityManager.OnUnequipItem += CancelEffects;
        }
    }

    public void CancelEffects()
    {
        DetermineCancelation(thisEffect);

        if (isEquipment)
            UtilityManager.OnUnequipItem -= CancelEffects;
    }

    private void DetermineEffect(CardEffect effect)
    {
        switch (effect.effectType)
        {
            case EffectTypes.DrawUtility:
                DrawUtility(effect.EffectMagnitude);
                return;

            case EffectTypes.DrawEvent:
                DrawEvent(effect.EffectMagnitude);
                return;


            case EffectTypes.ReorderEventDeck:
                ReorderEvents(effect.EffectMagnitude);
                return;

            case EffectTypes.ReorderUtilityDeck:
                ReorderUtility(effect.EffectMagnitude);
                return;

            case EffectTypes.DangerPointModifier:
                DangerPointModifier(effect.EffectMagnitude);
                return;

            case EffectTypes.PlayCountModifier:
                PlayCountModifier(effect.EffectMagnitude);
                return;

            case EffectTypes.DamageTakenModifier:
                ModifyDamageTaken(effect.EffectMagnitude);
                return;

            case EffectTypes.NullifyEventDamage:
                NullifyEventDamage(true);
                return;

            case EffectTypes.CycleEventDraw:
                CycleEventDraw(true);
                return;

            case EffectTypes.UtilityDrawModifier:
                UtilityDrawModifier(effect.EffectMagnitude);
                return;

            case EffectTypes.EventDrawModifier:
                EventDrawModifier(effect.EffectMagnitude);
                return;
            case EffectTypes.BrokenKatana:
                BrokenKatana(true);
                return;
        }
    }

    private void DetermineCancelation(CardEffect effect)
    {
        switch (effect.effectType)
        {
            case EffectTypes.ReorderEventDeck:
                ReorderEvents(-effect.EffectMagnitude);
                return;

            case EffectTypes.ReorderUtilityDeck:
                ReorderUtility(-effect.EffectMagnitude);
                return;

            case EffectTypes.DangerPointModifier:
                DangerPointModifier(-effect.EffectMagnitude);
                return;

            case EffectTypes.PlayCountModifier:
                PlayCountModifier(-effect.EffectMagnitude);
                return;

            case EffectTypes.DamageTakenModifier:
                ModifyDamageTaken(-effect.EffectMagnitude);
                return;

            case EffectTypes.NullifyEventDamage:
                NullifyEventDamage(false);
                return;

            case EffectTypes.CycleEventDraw:
                CycleEventDraw(false);
                return;

            case EffectTypes.UtilityDrawModifier:
                UtilityDrawModifier(-effect.EffectMagnitude);
                return;

            case EffectTypes.EventDrawModifier:
                EventDrawModifier(-effect.EffectMagnitude);
                return;

            case EffectTypes.BrokenKatana:
                BrokenKatana(false);
                return;
        }
    }

    private void CancelTimer()
    {
        currentDurationTimer++;

        if (currentDurationTimer >= thisEffect.EffectDuration)
        {
            CancelEffects();
            GameManager.OnStartNewTurn -= CancelTimer;
        }
    }

    private void DrawUtility(int magnitude)
    {
        Debug.Log("Drawing a card from a card effect.");
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
}
