using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject player;

    public event EventHandler OnRestartButtonClicked;
    public event EventHandler OnQuitButtonClicked;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        platformGameManager.Instance.OnStateChanged += platformGameManager_OnStateChanged;
        restartButton.onClick.AddListener(RestardButtonCliked);
        quitButton.onClick.AddListener(QuitButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
        scoreText = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.FindGameObjectWithTag("HighScoreUI").GetComponent<TextMeshProUGUI>();
        Hide();
    }

    private void platformGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (platformGameManager.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetString("currentScore", scoreText.text);
        highScoreText.text = PlayerPrefs.GetString("highScore", highScoreText.text);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void RestardButtonCliked()
    {
        Hide();
        OnRestartButtonClicked?.Invoke(this, EventArgs.Empty);
    }
    private void QuitButtonClicked()
    {
        OnQuitButtonClicked?.Invoke(this, EventArgs.Empty);
        Application.Quit();
    }

    private void MainMenuButtonClicked()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
    
}
