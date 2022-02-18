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

public enum UtilityEffectTypes
{
    DrawUtility,
    DrawEvent,
    UtilityDrawModifier,
    EventDrawModifier,
    ReorderUtilityDeck,
    ReorderEventDeck,
    DangerPointModifier,
    PlayCountModifier,
    NullifyEventDamage,
    DamageTakenModifier,
    MandatoryPlay,
    CycleEventDraw,
    BrokenKatana
}

public enum EventEffectTypes
{
    None,
    Clue,
    DestroyEquippedItem,
    PayItemToEvent,
    DamageTakenModifier,
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

public enum EventCardType
{
    None,
    Event,
    Clue
}

public enum UtilityPosition
{
    None,
    Hand,
    EquipmentSlot
}