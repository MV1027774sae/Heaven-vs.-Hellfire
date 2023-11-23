using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] bool isPaused;

    [SerializeField] GameObject leaderboardPanel;
    //[SerializeField] GameObject gamePausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        if(isPaused)
        {
            leaderboardPanel.SetActive(true);
            //gamePausePanel.SetActive(true);
        }
        else
        {
            leaderboardPanel.SetActive(false);
            //gamePausePanel.SetActive(false);
        }
    }
}
