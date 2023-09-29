using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool solo;
    public int numberOfUsers;
    public List<PlayerBase> players = new List<PlayerBase>(); //the list with all our players and 

    //the list were we hold anything we need to know for each separate character,
    //for now, it' s their id and their corresponding prefab
    public List<CharacterBase> characterList = new List<CharacterBase>();

    //we use this function to find characters from their id
    public CharacterBase returnCharacterWithID(string id)
    {
        CharacterBase retVal = null;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (string.Equals(characterList[i].charId, id))
            {
                retVal = characterList[i];
            }
        }

        return retVal;
    }

    //we use this one to return the player from their created chracter states
    public PlayerBase returnPlayerFromStates(StateManager states)
    {
        PlayerBase retVal = null;

        for ( int i = 0;i < /*states.*/players.Count; i++)
        {
            if (players[i].playerStates == states)
            {
                retVal = /*states.*/players[i];
            }
        }

        return retVal;
    }

    public PlayerBase returnOppositePlayer(PlayerBase pl)
    {
        PlayerBase retVal = null;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != pl)
            {
                retVal = players[i];
                break;
            }
        }

        return retVal;
    }

    public int ReturnCharacterInt(GameObject prefab)
    {
        int retVal = 0;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].prefab == prefab)
            {
                retVal = i;
                break;
            }
        }

        return retVal;
    }

    public static CharacterManager instance;
    public static CharacterManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

[System.Serializable]
public class CharacterBase
{
    public string charId;
    public GameObject prefab;
    public Sprite icon;
}

[System.Serializable]
public class PlayerBase
{
    public string playerId;
    public string inputId;
    public PlayerType playerType;
    public bool hasCharacter;
    public GameObject playerPrefab;
    public StateManager playerStates;
    public int score;

    public enum PlayerType
    {
        user, //a real human
        ai, //computer controlled
        simulation //for multiplayer over network
    }
}