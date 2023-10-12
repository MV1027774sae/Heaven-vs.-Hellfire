using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform[] spawnPositions; //the positions characters will spawn on

    CameraManager camM;
    CharacterManager charM;
    LevelUI levelUI; //we store ui elements here for ease of access

    public int maxTurns = 2;
    int currentTurn = 1; //the current turn we're on, starts at 1

    //variables for the countdown timer
    public bool countdown;
    public int maxTurnTimer = 99;
    private int currentTimer;
    private float internalTimer;

    void Start()
    {
        //get the references from the singletons
        charM = CharacterManager.GetInstance();
        levelUI = LevelUI.GetInstance();
        camM = CameraManager.GetInstance();

        levelUI.AnnouncerTextLine1.gameObject.SetActive(false);
        levelUI.AnnouncerTextLine2.gameObject.SetActive(false);

        StartCoroutine("StartGame");
    }
    private void FixedUpdate()
    {
        //a fast way to handle player orientation in the scene
        //just compare the x of the first player, and see if it' s lower than the enemy is on the right

        if (charM.players[0].playerStates.transform.position.x < charM.players[1].playerStates.transform.position.x)
        {
            charM.players[0].playerStates.lookRight = true;
            charM.players[1].playerStates.lookRight = false;
        }
        else
        {
            charM.players[0].playerStates.lookRight = false;
            charM.players[1].playerStates.lookRight = true;
        }
    }

    void Update()
    {
        if (countdown) //if we enable countdown
        {
            HandleTurnTimer(); //control the timer here
        }
    }

    void HandleTurnTimer()
    {
        levelUI.LevelTimer.text = currentTimer.ToString();

        internalTimer += Time.deltaTime; //every one second (frame dependant)

        if (internalTimer > 1)
        {
            currentTimer--; //subtract from the current timer once
            internalTimer = 0;
        }

        if (currentTimer <= 0) //if the countdown is over
        {
            EndTurnFunction(true); //end the turn
            countdown = false;
        }
    }

    IEnumerator StartGame()
    {
        //for when we first start the game

        //we need to create the players first
        yield return CreatePlayers();

        //then initialise the turn
        yield return InitTurn();
    }

    IEnumerator CreatePlayers()
    {
        //go to all the players we have n our list
        for (int i = 0; i < charM.players.Count; i++)
        {
            //and instantiate their prefabs
            GameObject go = Instantiate(
                charM.players[i].playerPrefab, spawnPositions[i].position, Quaternion.identity) as GameObject;

            //and assign the needed references
            charM.players[i].playerStates = go.GetComponent<StateManager>();

            charM.players[i].playerStates.healthSlider = levelUI.healthSliders[i];

            camM.players.Add(go.transform);
        }

        yield return null;
    }

    IEnumerator InitTurn()
    {
        //to init the turn

        //disable the announcer texts first
        levelUI.AnnouncerTextLine1.gameObject.SetActive(false);
        levelUI.AnnouncerTextLine2.gameObject.SetActive(false);

        //reset the timer
        currentTimer = maxTurnTimer;
        countdown = false;

        //start initialising the players
        yield return InitPlayers();

        //and then start the coroutine to enable the controls of each player
        yield return EnableControl();
    }

    IEnumerator InitPlayers()
    {
        //right now, the only thing we have to do is reset their health
        for (int i = 0; i < charM.players.Count; i++)
        {
            charM.players[i].playerStates.health = 100;
            charM.players[i].playerStates.handleAnim.anim.Play("Locomotion");
            charM.players[i].playerStates.transform.position = spawnPositions[i].position;
        }

        yield return null;
    }

    IEnumerator EnableControl()
    {
        //start with the announcer text

        levelUI.AnnouncerTextLine1.gameObject.SetActive(true);
        levelUI.AnnouncerTextLine1.text = "Turn " + currentTurn;
        levelUI.AnnouncerTextLine1.color = Color.white;

        yield return new WaitForSeconds(2);

        //change the UI text and color every second that passes
        levelUI.AnnouncerTextLine1.text = "3";
        levelUI.AnnouncerTextLine1.color = Color.green;
        yield return new WaitForSeconds(1);
        levelUI.AnnouncerTextLine1.text = "2";
        levelUI.AnnouncerTextLine1.color = Color.yellow;
        yield return new WaitForSeconds(1);
        levelUI.AnnouncerTextLine1.text = "1";
        levelUI.AnnouncerTextLine1.color = Color.red;
        yield return new WaitForSeconds(1);
        levelUI.AnnouncerTextLine1.text = "FIGHT!";
        levelUI.AnnouncerTextLine1.color = Color.red;

        //and for every player enable what they need to have open to be controlled
        for (int i = 0; i < charM.players.Count; i++)
        {
            //for user players, enable the input handler for example
            if (charM.players[i].playerType == PlayerBase.PlayerType.user)
            {
                InputHandler ih = charM.players[i].playerStates.gameObject.GetComponent<InputHandler>();
                ih.playerInput = charM.players[i].inputId;
                ih.enabled = true;
            }

            //if it's an AI character
            if (charM.players[i].playerType == PlayerBase.PlayerType.ai)
            {
                AICharacter ai = charM.players[i].playerStates.gameObject.GetComponent<AICharacter>();
                ai.enabled = true;

                //assign the enemy states to be the one from the opposite player
                ai.enStates = charM.returnOppositePlayer(charM.players[i]).playerStates;
            }
        }

        //after a second, diable the announcer text
        yield return new WaitForSeconds(1);
        levelUI.AnnouncerTextLine1.gameObject.SetActive(false);
        countdown = true;
    }

    void DisableControl()
    {
        //to disable the controls, you need to disable the component that makes a character
        for (int i = 0; i < charM.players.Count; ++i)
        {
            //but first, reset the variables in their state manager
            charM.players[i].playerStates.ResetStateInputs();

            //for user players, that's the input handler
            if (charM.players[i].playerType == PlayerBase.PlayerType.user)
            {
                charM.players[i].playerStates.GetComponent<InputHandler>().enabled = false;
            }
        }
    }

    public void EndTurnFunction(bool timeOut = false)
    {
        //we calll this function everytime we want end the turn but we need to know if we do so by a timeout or not
        countdown = false;
        //reset the timer text
        levelUI.LevelTimer.text = maxTurnTimer.ToString();

        //if it's a timeout
        if (timeOut)
        {
            //add this text first
            levelUI.AnnouncerTextLine1.gameObject.SetActive(true);
            levelUI.AnnouncerTextLine1.text = "Time Out!";
            levelUI.AnnouncerTextLine1.color = Color.blue;
        }
        else
        {
            levelUI.AnnouncerTextLine1.gameObject.SetActive(true);
            levelUI.AnnouncerTextLine1.text = "K.O.";
            levelUI.AnnouncerTextLine1.color = Color.red;
        }

        //disable the controls
        DisableControl();

        //and start the coroutine for the end turn
        StartCoroutine("EndTurn");
    }

    IEnumerator EndTurn()
    {
        //wait for the previous text to clearly show
        yield return new WaitForSeconds(3);

        //find who was the player that won
        PlayerBase vPlayer = FindWinningPlayer();

        if (vPlayer == null ) //if our functyion returned a null
        {
            //that means it's a draw
            levelUI.AnnouncerTextLine1.text = "Draw";
            levelUI.AnnouncerTextLine1.color = Color.blue;
        }
        else
        {
            //else that player is the winner
            levelUI.AnnouncerTextLine1.text = vPlayer.playerId + " Wins!";
            levelUI.AnnouncerTextLine1.color = Color.yellow;
        }

        yield return new WaitForSeconds(3);

        //check to see if the victorious player has taken any damage
        if (vPlayer != null)
        {
            //if not, then it's a perfect victory
            if (vPlayer.playerStates.health == 100)
            {
                levelUI.AnnouncerTextLine2.gameObject.SetActive(true);
                levelUI.AnnouncerTextLine2.text = "PERFECT";
            }
        }

        yield return new WaitForSeconds(3);

        currentTurn++; //add to the turn counter

        bool matchOver = isMatchOver();

        if (!matchOver)
        {
            StartCoroutine("InitTurn"); //and start the loop for the next turn again
        }
        else
        {
            for (int i = 0; i < charM.players.Count; i++)
            {
                charM.players[i].score = 0;
                charM.players[i].hasCharacter = false;
            }
            if (charM.solo)
            {
                if (vPlayer == charM.players[0])
                {
                    MySceneManager.GetInstance().LoadNextOnProgression();
                }
                else
                {
                    MySceneManager.GetInstance().RequestLevelLoad(SceneType.main, "game_over");
                }
            }
            else
            {
                MySceneManager.GetInstance().RequestLevelLoad(SceneType.main, "select");
            }
        }
    }

    private bool isMatchOver()
    {
        bool retVal = false;

        for (int i = 0; i < charM.players.Count; i++)
        {
            if (charM.players[i].score >= maxTurns)
            {
                retVal = true;
                break;
            }
        }

        return retVal;
    }

    PlayerBase FindWinningPlayer()
    {
        //to find who won the turn
        PlayerBase retVal = null;

        StateManager targetPlayer = null;

        //check first to see if both players have equal health
        if (charM.players[0].playerStates.health != charM.players[1].playerStates.health)
        {
            //if not, the check who has the lower health, the other one is the winner
            if (charM.players[0].playerStates.health < charM.players[1].playerStates.health)
            {
                charM.players[1].score++;
                targetPlayer = charM.players[1].playerStates;
                levelUI.AddWinIndicator(1);
            }
            else
            {
                charM.players[0].score++;
                targetPlayer = charM.players[0].playerStates;
                levelUI.AddWinIndicator(0);
            }

            retVal = charM.returnPlayerFromStates(targetPlayer);
        }

        return retVal;
    }

    public static LevelManager instance;
    public static LevelManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
}
