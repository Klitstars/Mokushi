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
    CycleEventDraw,
}

public enum EffectTargets
{
    None,
    CurrentEquipment,
    BrokenKatana,
    Katana,
    Shurikens,
    GrapplingHook

}

public enum EventEffectTypes
{
    Clue,
    DestroyEquippedItem,
    PayItemToEvent,
    DamageTakenModifier,
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