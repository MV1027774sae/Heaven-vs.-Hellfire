using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPoint : MonoBehaviour
{
    [SerializeField] Scores scoresScript;
    [SerializeField] DetectController detectControllerScript;
    [SerializeField] HighscoreHandler HighscoreHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && detectControllerScript.gameStopped == false)
        {
            scoresScript.scores++;
            HighscoreHandlerScript.SetHigherscore(scoresScript.scores);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            detectControllerScript.gameStopped = true;
            HighscoreHandlerScript.SetLastestHighscore();
        }
    }
}
