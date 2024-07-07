using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        Time.timeScale = 1f;
    }
    public void PlayGame()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
