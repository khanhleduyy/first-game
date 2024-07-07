using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class platformGameManager : MonoBehaviour
{

    public static platformGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerContainer;
    private int playerNumber;
    private GameObject[] cardUIArray;
    private CardUI cardUI;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        BreakBetweenWaves,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 30f;
    private float gamePlayingTimerMax = 30f;
    private float countdownToStartTimerMax = 3f;
    private bool isGamePaused = false;


    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        playerNumber = playerContainer.transform.childCount;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if(playerNumber < 1)
                {
                    Instantiate(player, playerContainer.transform.position, playerContainer.transform.rotation, playerContainer.transform);
                    playerNumber++;
                }
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.BreakBetweenWaves;
                    
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    
                }
                if (Player2.Instance.IsDeath())
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }

                break;
            case State.BreakBetweenWaves:
                cardUIArray = GameObject.FindGameObjectsWithTag("CardUI");
                foreach (GameObject card in cardUIArray)
                {
                    cardUI = card.GetComponent<CardUI>();
                    if (cardUI != null)
                    {
                        cardUI.OnCardPick += CardUI_OnCardPick1;
                        
                    }
                    
                }
                break;
            case State.GameOver:
                GameOverUI.Instance.OnRestartButtonClicked += GameOverUI_OnRestartButtonClicked;
                break;
        }
        
    }

    private void GameOverUI_OnRestartButtonClicked(object sender, EventArgs e)
    {
        playerNumber = 0;
        state = State.CountDownToStart;
        gamePlayingTimer = gamePlayingTimerMax;
        countdownToStartTimer = countdownToStartTimerMax;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void CardUI_OnCardPick1(object sender, EventArgs e)
    {
        state = State.GamePlaying;
        gamePlayingTimer = gamePlayingTimerMax;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;  
    }

    public bool IsBreakBetweenWaves()
    {
        return state == State.BreakBetweenWaves;
    }

    public float GetGamePlayingTime()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);

        }
    }

}
