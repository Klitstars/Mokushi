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
    CycleEventDraw,
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

public enum UtilityPosition
{
    None,
    Hand,
    EquipmentSlot
}