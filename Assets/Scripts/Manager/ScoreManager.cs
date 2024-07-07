using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static int scoreValue = 0;
    public GameObject score;
    public GameObject highScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //score = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TextMeshProUGUI>();
        //highScore = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TextMeshProUGUI>();
        //score = GetComponent<TextMeshProUGUI>();
        /*
        foreach (GameObject score in score)
        {
            scoreText = score.GetComponent<TextMeshProUGUI>();
        }
        */
        scoreText = score.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        GameOverUI.Instance.OnRestartButtonClicked += GameOverUI_OnRestartButtonClicked;
        GameOverUI.Instance.OnQuitButtonClicked += GameOverUI_OnQuitButtonClicked;
        GamePausedUI.Instance.OnMainmenuButtonClicked += GamePausedUI_OnMainmenuButtonClicked;
        //highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void GamePausedUI_OnMainmenuButtonClicked(object sender, System.EventArgs e)
    {
        scoreValue = 0;
    }

    private void GameOverUI_OnRestartButtonClicked(object sender, System.EventArgs e)
    {
        scoreValue = 0;
    }
    private void GameOverUI_OnQuitButtonClicked(object sender, System.EventArgs e)
    {
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + scoreValue;

        if(scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScoreText.text = "High Score: " + scoreValue.ToString();
        }
        
        
    }
    // delete highscore
    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");    
    }
}
