using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SelectScreenManager : MonoBehaviour
{
    public int numberOfPlayers = 1;
    public List<PlayerInterfaces> plInterfaces = new List<PlayerInterfaces>();
    public PortraitInfo[] portraitPrefabs; //all our entries as portraits
    public int maxX; //how many portraits we have on the X and Y NOTE: this is hardcoded!
    public int maxY;
    PortraitInfo[,] charGrid; //the grid we are making to select entries
    private int maxRow;
    private int maxCollum;
    List<PortraitInfo> portraitList = new List<PortraitInfo>();

    public GameObject portraitCanvas; //the canvas that holds all the portraits

    private bool loadLevel; //if we are loading the level
    public bool bothPlayersSelected;

    CharacterManager charM;

    GameObject portraitPrefab;

    #region Singleton
    public static SelectScreenManager instance;
    public static SelectScreenManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        //we start by getting the reference to the character manager
        charM = CharacterManager.GetInstance();
        numberOfPlayers = charM.numberOfUsers;

        portraitPrefab = Resources.Load("portraitPrefab") as GameObject;
        CreatePortraits();

        charM.solo = (numberOfPlayers == 1);

        ////and we create the grid
        //charGrid = new PortraitInfo[maxX, maxY];

        //int x = 0;
        //int y = 0;

        //portraitPrefabs = portraitCanvas.GetComponentsInChildren<PortraitInfo>();

        ////we need to go into all our portraits
        //for (int i = 0; i < portraitPrefabs.Length; i++)
        //{
        //    //and assign a grid position
        //    portraitPrefabs[i].posX = x;
        //    portraitPrefabs[i].posY = y;

        //    charGrid[x, y] = portraitPrefabs[i];

        //    if (x <  maxX - 1)
        //    {
        //        x++;
        //    }
        //    else
        //    {
        //        x = 0;
        //        y++;
        //    }
        //}
    }

    void CreatePortraits()
    {
        GridLayoutGroup group = portraitCanvas.GetComponent<GridLayoutGroup>();

        maxRow = group.constraintCount;
        int x = 0;
        int y = 0;

        for (int i = 0; i < charM.characterList.Count; i++)
        {
            CharacterBase c = charM.characterList[i];

            GameObject go = Instantiate(portraitPrefab) as GameObject;
            go.transform.SetParent(portraitCanvas.transform);

            PortraitInfo p = go.GetComponent<PortraitInfo>();
            //p.img.sprite = c.icon; //TODO: creates error, find a fix
            p.characterId = c.charId;
            p.posX = x;
            p.posY = y;
            portraitList.Add(p);

            if (x < maxRow - 1)
            {
                x++;
            }
            else
            {
                x = 0;
                y++;
            }

            maxCollum = y;
        }
    }

    void Update()
    {
        if (!loadLevel)
        {
            for (int i = 0; i < plInterfaces.Count; i++)
            {
                if (i < numberOfPlayers)
                {
                    if (Input.GetButtonUp("Fire2" + charM.players[i].inputId))
                    {
                        plInterfaces[i].playerBase.hasCharacter = false;
                    }

                    if (!charM.players[i].hasCharacter)
                    {
                        plInterfaces[i].playerBase = charM.players[i];

                        HandleSelectorPosition(plInterfaces[i]);
                        HandleSelectScreenInput(plInterfaces[i], charM.players[i].inputId);
                        HandleCharacterPreview(plInterfaces[i]);
                    }
                }
                else
                {
                    charM.players[i].hasCharacter = true;
                }
            }
        }
        if (bothPlayersSelected)
        {
            Debug.Log("loading");
            StartCoroutine("LoadLevel"); //and start the coroutine to load the level
            loadLevel = true;
        }
        else
        {
            if (charM.players[0].hasCharacter && charM.players[1].hasCharacter)
            {
                bothPlayersSelected = true;
            }
        }
    }

    void HandleSelectScreenInput(PlayerInterfaces pl, string playerId)
    {
        #region Grid Navigation
        /*to navigate in the grid
         * we simply change the active x and y to select what entry is active
         * we also smooth our the input so if the user keeps pressing the button
         * it won' t switch more than once over half a second
         */

        float vertical = Input.GetAxis("Vertical" + playerId);

        if (vertical != 0)
        {
            if (!pl.hitInputOnce)
            {
                if (vertical > 0)
                {
                    pl.activeY = (pl.activeY > 0) ? pl.activeY - 1 : maxCollum;
                }
                else
                {
                    pl.activeY = (pl.activeY < maxCollum) ? pl.activeY + 1 : 0;
                }

                pl.hitInputOnce = true;
            }
        }

        float horizontal = Input.GetAxis("Horizontal" + playerId);

        if (horizontal != 0)
        {
            if (!pl.hitInputOnce)
            {
                if (horizontal > 0)
                {
                    pl.activeX = (pl.activeX > 0) ? pl.activeX - 1 : maxRow - 1;
                }
                else
                {
                    pl.activeX = (pl.activeX < maxRow - 1) ? pl.activeX + 1 : 0;
                }

                pl.timerToReset = 0;
                pl.hitInputOnce = true;
            }
        }

        if (vertical == 0 && horizontal == 0)
        {
            pl.hitInputOnce = false;
        }

        if (pl.hitInputOnce)
        {
            pl.timerToReset += Time.deltaTime;

            if (pl.timerToReset > 0.8f)
            {
                pl.hitInputOnce = false;
                pl.timerToReset = 0;
            }
        }

        #endregion

        //if the user presses space, they have selected a character
        if (Input.GetButtonUp("Fire1" + playerId))
        {
            //make a reaction on the character to give feedback to the player
            //pl.createdCharacter.GetComponentInChildren<Animator>().Play("Kick");

            //pass the character to the character manager so that we know what prefab to create in the level
            pl.playerBase.playerPrefab =
                charM.returnCharacterWithID(pl.activePortrait.characterId).prefab;

            pl.playerBase.hasCharacter = true;
        }
    }

    IEnumerator LoadLevel()
    {
        //if any of the players is an AI, then assign a random character to the prefab
        for (int i = 0; i < charM.players.Count; i++)
        {
            if (charM.players[i].playerType == PlayerBase.PlayerType.ai)
            {
                if (charM.players[i].playerPrefab == null)
                {
                    int ranVal = Random.Range(0, portraitPrefabs.Length);

                    charM.players[i].playerPrefab =
                        charM.returnCharacterWithID(portraitPrefabs[ranVal].characterId).prefab;

                    Debug.Log(portraitPrefabs[ranVal].characterId);
                }
            }
        }

        yield return new WaitForSeconds(2); //after 2 seconds load the level
        //SceneManager.LoadSceneAsync("level", LoadSceneMode.Single);

        if (charM.solo)
        {
            MySceneManager.GetInstance().CreateProgression();
            MySceneManager.GetInstance().LoadNextOnProgression();
        }
        else
        {
            //MySceneManager.GetInstance().RequestLevelLoad(SceneType.prog, "level_1");
            MySceneManager.GetInstance().RequestLevelLoad(SceneType.prog, MySceneManager.GetInstance().levels[Random.Range(0,2)]);
        }
    }

    void HandleSelectorPosition(PlayerInterfaces pl)
    {
        pl.selector.SetActive(true); //enable the selector

        PortraitInfo pi = ReturnPortrait(pl.activeX, pl.activeY); //find the active portrait

        if (pi != null)
        {
            pl.activePortrait = pi; //find the active portrait

            //and place the selector over it's position
            Vector2 selectorPosition = pl.activePortrait.transform.localPosition;

            selectorPosition = selectorPosition + new Vector2(portraitCanvas.transform.localPosition.x, portraitCanvas.transform.localPosition.y);
            pl.selector.transform.localPosition = selectorPosition;
        }
    }

    void HandleCharacterPreview(PlayerInterfaces pl)
    {
        //if the previews portrait we had is not the same as the active one we have
        //that means we changed characters
        if (pl.previewPortrait != pl.activePortrait)
        {
            if (pl.createdCharacter != null) //delete one we have now if we do not have one
            {
                Destroy(pl.createdCharacter);
            }

            //and create another one
            GameObject go = Instantiate(
                CharacterManager.GetInstance().returnCharacterWithID(pl.activePortrait.characterId).prefab,
                pl.charVisPos.position, Quaternion.identity) as GameObject;
            pl.createdCharacter = go;

            pl.previewPortrait = pl.activePortrait;

            if (!string.Equals(pl.playerBase.playerId, charM.players[0].playerId))
            {
                pl.createdCharacter.GetComponent<StateManager>().lookRight = false;
            }
        }
    }

    PortraitInfo ReturnPortrait(int x, int y)
    {
        PortraitInfo r = null;
        for (int i = 0; i < portraitList.Count; i++)
        {
            if (portraitList[i].posX == x && portraitList[i].posY == y)
            {
                r = portraitList[i];
            }
        }

        return r;
    }

    [System.Serializable]
    public class PlayerInterfaces
    {
        public PortraitInfo activePortrait; //the current active portrait for player 1
        public PortraitInfo previewPortrait;
        public GameObject selector; //the select indicator for player 1
        public Transform charVisPos; //the visualisation position for player 1
        public GameObject createdCharacter; //the created character for player 1

        //the active X and Y entries for player 1
        public int activeX;
        public int activeY;

        //variables for smoothing out input
        public bool hitInputOnce;
        public float timerToReset;

        public PlayerBase playerBase;
    }
}
