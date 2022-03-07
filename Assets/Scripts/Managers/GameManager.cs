using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingDrawCount;

    private int utilityDrawModifier = 0;
    private int eventDrawModifier = 0;
    private int damageModifier = 0;
    private int currentTurnCount = 1;
    private int clueCount = 0;
    private bool gameStarted = false;

    public static GameManager instance;

    private UtilityManager utilityManager;
    private EventManager eventManager;
    private CardUIPlayController cardUIPlayController;
    private DeckManager deckManager;
    private CardPlayController cardPlayController;
    private CardUIBuilder cardUIBuilder;
    private WeaponSelectController weaponSelectController;

    public bool keepCurrentEvent = false;
    public bool nullifyDamage = true;
    [SerializeField] private int canEndTurn = 0;

    public UtilityManager UtilityManager { get => utilityManager; }
    public EventManager EventManager { get => eventManager; }
    public CardUIPlayController CardUIPlayController { get => cardUIPlayController; }
    public CardUIBuilder CardUIBuilder { get => cardUIBuilder;}
    public DeckManager DeckManager { get => deckManager; }
    public CardPlayController CardPlayController { get => cardPlayController; }
    public WeaponSelectController WeaponSelectController { get => weaponSelectController; }


    public delegate void onStartNewTurn();
    public static event onStartNewTurn OnStartNewTurn;
    public delegate void onEndTurn();
    public static event onEndTurn OnEndTurn;

    public void UpdateDrawModifier(int amountToModify) => utilityDrawModifier += amountToModify;
    public void UpdateDamageModifier(int amountToModify) => damageModifier += damageModifier;

    public void UpdatePlayerHealth(int amountToModify)
    {
        currentHealth += (amountToModify + damageModifier);

        cardUIPlayController.UpdatePlayerHealth(currentHealth);
    }

    public void UpdateClueCount(int amountToModify)
    {
        clueCount += amountToModify;
        cardUIPlayController.UpdateClueCount(clueCount);

        if (clueCount >= 4)
            CardUIPlayController.GameOver(true);
    }

    public void CanEndTurn(int endTurn)
    {
        canEndTurn += endTurn;

        if (canEndTurn < 0)
            CardUIPlayController.CanEndTurn(false);
        if (canEndTurn >= 0)
            CardUIPlayController.CanEndTurn(true);
            
    }

    public void StartGame()
    {
        eventManager.DrawCardAndUpdateEvents();
        utilityManager.DrawCards(7);
        cardUIPlayController.UpdatePlayerHealth(currentHealth);

        OnStartNewTurn += EventManager.DrawCardAndUpdateEvents;
        OnStartNewTurn += UtilityManager.DrawCard;
        OnStartNewTurn += UpdateTurnCount;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void EndTurn()
    {
        if(OnEndTurn != null)
            OnEndTurn.Invoke();

        if (!keepCurrentEvent)
            if(!SurvivedEvent())
            {
                LoseGame();
                return;
            }

        if(OnStartNewTurn != null)
            OnStartNewTurn.Invoke();
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitGameManager();
    }

    private void InitGameManager()
    {
        if (utilityManager == null)
            utilityManager = FindObjectOfType<UtilityManager>();
        if (eventManager == null)
            eventManager = FindObjectOfType<EventManager>();
        if (cardUIPlayController == null)
            cardUIPlayController = FindObjectOfType<CardUIPlayController>();
        if (deckManager == null)
            deckManager = FindObjectOfType<DeckManager>();
        if (cardPlayController == null)
            cardPlayController = FindObjectOfType<CardPlayController>();
        if (cardUIBuilder == null)
            cardUIBuilder = FindObjectOfType<CardUIBuilder>();
        if (weaponSelectController == null)
            weaponSelectController = FindObjectOfType<WeaponSelectController>();

        if (currentHealth != maxHealth)
            currentHealth = maxHealth;
    }

    private bool SurvivedEvent()
    {
        eventManager.EndTurnCheck(nullifyDamage);

        if (currentHealth <= 0)
            return false;
        return true;
    }

    private void LoseGame()
    {
        CardUIPlayController.GameOver(false);
    }

    private void UpdateTurnCount()
    {
        currentTurnCount++;
        cardUIPlayController.UpdateTurnCount(currentTurnCount);
    }
}
