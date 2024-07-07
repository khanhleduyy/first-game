using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GamePausedUI : MonoBehaviour
{
    public static GamePausedUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;

    public event EventHandler OnMainmenuButtonClicked;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeButton);
        mainMenuButton.onClick.AddListener(MainMenuButton);
        musicButton.onClick.AddListener(MusicButton);
    }

    private void Start()
    {
        platformGameManager.Instance.OnGamePaused += platformGameManager_OnGamePaused;
        platformGameManager.Instance.OnGameUnpaused += platformGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void platformGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void platformGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ResumeButton()
    {
        platformGameManager.Instance.PauseGame();
    }

    private void MainMenuButton()
    {
        OnMainmenuButtonClicked?.Invoke(this, EventArgs.Empty);
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void MusicButton()
    {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

}
