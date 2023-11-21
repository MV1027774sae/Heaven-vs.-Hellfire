using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    public int highscore;

    [SerializeField] HighScoreUI highScoreUIScript;

    public int HighestScore
    {
        set
        {
            highscore = value;
            highScoreUIScript.SetHighscoreUI(value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetLastestHighscore();
    }

    public void SetLastestHighscore()
    {
        HighestScore = PlayerPrefs.GetInt("Highscore", 0);
    }

    void SaveHighscore(int score)
    {
        PlayerPrefs.SetInt("Highscore", score);
    }

    public void SetHigherscore(int score)
    {
        if(score > highscore)
        {
            HighestScore = score;
            SaveHighscore(score);
        }
    }
}
