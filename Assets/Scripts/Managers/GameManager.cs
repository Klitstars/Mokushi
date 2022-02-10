using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int turnDrawCount;
    private List<SOCardBase> currentHand;
    
    public static GameManager instance;

    private UtilityManager utilityManager;
    private EventManager eventManager;
    private HandManager handManager;

    public bool keepCurrentEvent = false;

    public UtilityManager UtilityManagerInstance { get => utilityManager; }
    public EventManager EventManagerInstance { get => eventManager; }
    public HandManager HandManagerInstance { get => handManager; }


    public delegate void onStartNewTurn();
    public static event onStartNewTurn OnStartNewTurn;

    public void EndTurn()
    {
        if (!keepCurrentEvent)
        {
            if(SurvivedEvent())
                eventManager.DrawAndUpdateEvent();
            else
            {
                LoseGame();
                return;
            }
        }

        utilityManager.DrawCard(turnDrawCount);
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

        if (currentHealth != maxHealth)
            currentHealth = maxHealth;
    }

    private bool SurvivedEvent()
    {
        maxHealth -= eventManager.CurrentEventDanger;

        if (maxHealth <= 0)
            return false;

        return true;
    }

    private void LoseGame()
    {
        Debug.Log("Lost the game.");
        //Do something here.
    }
}
