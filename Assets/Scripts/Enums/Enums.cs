public enum CardType
{
    Utility,
    Event
}


public enum UtilityType
{
    None,
    Equipment,
    Ninjutsu,
    Attribute
}

public enum EffectTypes
{
    //Non-scaling effects
    None,
    Clue,
    NullifyEventDamage,
    BrokenKatana,
    GrapplingHook,
    DestroyEquippedItem,
    PayItemToEvent,

    //Scaling effects
    DrawUtility,
    DrawEvent,
    UtilityDrawModifier,
    EventDrawModifier,
    DangerPointModifier,
    PlayCountModifier,
    DamageTakenModifier,
    ReorderUtilityDeck,
    ReorderEventDeck,
    CycleEventDraw,
    CycleUtilityDraw
}

public enum Equipment
{
    None,
    Any,
    BrokenKatana,
    Katana,
    Shurikens,
    GrapplingHook
}

public enum EffectTiming
{
    Immediate,
    EnactAtTurnStart,
    EnactAtTurnEnd
}

public enum CardPosition
{
    None,
    Hand,
    EquipmentSelect,
    EquipmentSlot
}