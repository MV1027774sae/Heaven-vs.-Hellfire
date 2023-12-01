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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            leaderboardPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
