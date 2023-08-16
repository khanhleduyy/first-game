using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreHandle : MonoBehaviour
{
    public GameObject score;
    public GameObject highScore;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = score.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();

        scoreText.text = "Score: " + PlayerPrefs.GetString("currentScore", scoreText.text);
        highScoreText.text = PlayerPrefs.GetString("highScore", highScoreText.text);
    }

    
}
