using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingDrawCount;
    private int utilityDrawModifier = 0;
    private int eventDrawModifier = 0;
    private int damageModifier = 0;
    public static GameManager instance;

    private UtilityManager utilityManager;
    private EventManager eventManager;
    private HandManager handManager;
    private DeckManager deckManager;
    private CardBuilder cardBuilder;
    private CardPlayController cardPlayController;

    public bool keepCurrentEvent = false;
    public bool nullifyDamage = true;
    public bool canEndTurn = true;

    public UtilityManager UtilityManager { get => utilityManager; }
    public EventManager EventManager { get => eventManager; }
    public HandManager HandManager { get => handManager; }
    public DeckManager DeckManager { get => deckManager; }
    public CardBuilder CardBuilder { get => cardBuilder; }
    public CardPlayController CardPlayController { get => cardPlayController; }


    public delegate void onStartNewTurn();
    public static event onStartNewTurn OnStartNewTurn;

    public void UpdateDrawModifier(int amountToModify) => utilityDrawModifier += amountToModify;
    public void UpdateDamageModifier(int amountToModify) => damageModifier += damageModifier;
    public void UpdatePlayerHealth(int amountToModify)
    {
        if (!nullifyDamage)
            currentHealth += amountToModify;

        Debug.Log("Current player health is: " + currentHealth);
    }

    [ContextMenu("StartGame")]
    public void StartGame()
    {
        utilityManager.DrawCards(7);
        eventManager.DrawAndUpdateEvents(1);

        OnStartNewTurn += UtilityManager.DrawCard;
        OnStartNewTurn += EventManager.DrawAndUpdateEvents;
    }

    public void EndTurn()
    {
        if (!keepCurrentEvent)
            if(!SurvivedEvent())
            {
                LoseGame();
                return;
            }

        OnStartNewTurn.Invoke();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        InitGameManager();
    }

    private void InitGameManager()
    {
        if (utilityManager == null)
            utilityManager = FindObjectOfType<UtilityManager>();
        if (eventManager == null)
            eventManager = FindObjectOfType<EventManager>();
        if (handManager == null)
            handManager = FindObjectOfType<HandManager>();
        if (deckManager == null)
            deckManager = FindObjectOfType<DeckManager>();
        if (cardBuilder == null)
            cardBuilder = FindObjectOfType<CardBuilder>();
        if (cardPlayController == null)
            cardPlayController = FindObjectOfType<CardPlayController>();

        if (currentHealth != maxHealth)
            currentHealth = maxHealth;
    }

    private bool SurvivedEvent()
    {
        eventManager.EndTurnCheck(nullifyDamage);

        if (currentHealth <= 0)
        {
            Debug.Log("Did not survive the event");
            return false;
        }

        Debug.Log("Survived the event");
        return true;
    }

    private void LoseGame()
    {
        Debug.Log("Lost the game.");
        //Do something here.
    }
}
