using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToChangeGameobject : MonoBehaviour
{
    public GameObject player1Panel;
    public GameObject player2Panel;

    public bool isPlayer1;

    // Update is called once per frame
    private void Start()
    {
        isPlayer1 = true;
    }
    void Update()
    {
        if(isPlayer1 == true)
        {
            player1Panel.SetActive(true);
            player2Panel.SetActive(false);
        }
        else
        {
            player1Panel.SetActive(false);
            player2Panel.SetActive(true);
        }
    }

    public void Open1()
    {
        isPlayer1 = true;
    }

    public void Open2()
    {
        isPlayer1 = false;
    }
}
