using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortraitInfo : MonoBehaviour {

    public int posX;
    public int posY;
    public string characterId;
    public Image img;

    private void Start()
    {
        if (characterId == "c2 Draft")
        {
            gameObject.transform.localScale = new Vector3 (-1.5f, 2.625f, 0.4166666f);
        }
    }
}
