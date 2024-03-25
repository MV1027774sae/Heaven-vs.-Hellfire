using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public GameObject startText;
    private float timer;
    private bool loadingLevel;
    private bool init;

    public int activeElement;
    public GameObject menuObj;
    public ButtonRef[] menuOptions;

    [SerializeField] bool checkIfTheObjectHaveBoolean;

    [SerializeField] GameObject image1;
    [SerializeField] GameObject image2;
    [SerializeField] GameObject image3;

    void Start()
    {
        menuObj.SetActive(false);
    }

    void Update()
    {
        if (!init)
        {
            //it flickers the "Press Start" text
            timer += Time.deltaTime;
            if (timer > 0.6f)
            {
                timer = 0;
                startText.SetActive(!startText.activeInHierarchy);
            }

            //where start == space
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
            {
                init = true;
                startText.SetActive(false);
                menuObj.SetActive(true); //closes the text and opens the menu
            }
        }
        else
        {
            if (!loadingLevel) //if not already loading the level
            {
                //indicate the selected option
                menuOptions[activeElement].selected = true;

                //change the selected option based on input
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    menuOptions[activeElement].selected = false;

                    if (activeElement > 0)
                    {
                        activeElement--;
                    }
                    else
                    {
                        activeElement = menuOptions.Length - 1;
                    }
                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    menuOptions[activeElement].selected = false;

                    if (activeElement < menuOptions.Length - 1)
                    {
                        activeElement++;
                    }
                    else
                    {
                        activeElement = 0;
                    }
                }

                //and if we hit space again
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
                {
                    if(activeElement == 2)
                    {
                        Debug.Log("TUrtorial!");
                    }
                    //then load the level
                    Debug.Log("load");
                    loadingLevel = true;
                    StartCoroutine("LoadLevel");
                    menuOptions[activeElement].transform.localScale *= 1.2f;

                    //and based on our selection
                    //TODO: 2 - players
                }

                //checking if what option the player currently on = change image
                if(activeElement == 0)
                {
                    image1.SetActive(true);
                    image2.SetActive(false);
                    image3.SetActive(false);
                }
                else if(activeElement == 1)
                {
                    image1.SetActive(false);
                    image2.SetActive(true);
                    image3.SetActive(false);
                }
                else if(activeElement == 2)
                {
                    image1.SetActive(false);
                    image2.SetActive(false);
                    image3.SetActive(true);
                }
                else
                {
                    image1.SetActive(false);
                    image2.SetActive(false);
                    image3.SetActive(false);
                }
            }
        }
    }

    void HandleSelectedOption()
    {
        switch (activeElement)
        {
            case 0:
                CharacterManager.GetInstance().numberOfUsers = 2;
                CharacterManager.GetInstance().players[1].playerType = PlayerBase.PlayerType.user;
                break;
        }
    }

    IEnumerator LoadLevel()
    {
        HandleSelectedOption();
        yield return new WaitForSeconds(0.6f);

        if (activeElement == 1)
        {
            MySceneManager.GetInstance().RequestLevelLoad(SceneType.main, "turtorial_level");
        }
        else if (activeElement == 2)
        {
            Debug.Log("Credit Scene");
            MySceneManager.GetInstance().RequestLevelLoad(SceneType.main, "credit");
        }
        else if (activeElement == 3)
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
        else
        {
            MySceneManager.GetInstance().RequestLevelLoad(SceneType.main, "select");
        }
    }
}
