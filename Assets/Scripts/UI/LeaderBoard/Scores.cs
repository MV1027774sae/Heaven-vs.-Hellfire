using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    public int scores;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        UpdateHUD();
    }

    public int Points
    {
        get
        {
            return scores;
        }
        set
        {
            scores = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        scoreText.text = scores.ToString();
    }
}
