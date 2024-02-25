using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] bool isPaused = false;

    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] bool isPanelOpen = false;
    [SerializeField] bool isLeaderboardOpen = false;

    [SerializeField] GameObject panel;

    [SerializeField] GameObject keyboardPanel;
    [SerializeField] GameObject controlPanel;

    [SerializeField] DetectController detectController;

    void Update()
    {
        //Open leaderboard
        if (Input.GetKeyDown(KeyCode.P)) isLeaderboardOpen = !isLeaderboardOpen;
        if(isLeaderboardOpen)
        {
            leaderboardPanel.SetActive(true);
            isPaused = true;
        }
        else
        {
            leaderboardPanel.SetActive(false);
            isPaused = false;
        }

        //Open panel
        if (Input.GetKeyDown(KeyCode.I)) isPanelOpen = !isPanelOpen;
        if (isPanelOpen)
        {
            panel.SetActive(true);
            isPaused = true;
        }
        else
        {
            panel.SetActive(false);
            isPaused = false;
        }

        if (detectController.isConnect)
        {
            keyboardPanel.SetActive(false);
            controlPanel.SetActive(true);
        }
        else
        {
            keyboardPanel.SetActive(true);
            controlPanel.SetActive(false);
        }

        //Lock and Unlock mouse cursoe
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
