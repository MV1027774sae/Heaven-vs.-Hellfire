using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;

    public void SetHighscoreUI(int score)
    {
        highscoreText.text = score.ToString();
    }
}
