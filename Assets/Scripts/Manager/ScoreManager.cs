using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static int scoreValue = 0;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TextMeshProUGUI>();
        //highScore = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TextMeshProUGUI>();
        //score = GetComponent<TextMeshProUGUI>();
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + scoreValue;

        if(scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreValue);
            highScore.text = "High Score: " + scoreValue.ToString();
        }
        
        
    }
    // delete highscore
    public void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");    
    }
}
