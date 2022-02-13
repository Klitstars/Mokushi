public enum CardType
{
    Utility,
    Event
}
public enum Utility
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
    Clue,
    CycleEventDraw,
    DestroyEquippedItem,
    PayItemToEvent
}

public enum Equipment
{
    None,
    BrokenKatana,
    Katana,
    Shurikens,
    GrapplingHook
}

public enum EventCardType
{
    Event,
    Clue
}