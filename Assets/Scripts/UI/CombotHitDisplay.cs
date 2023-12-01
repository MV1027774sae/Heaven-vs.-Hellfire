using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombotHitDisplay : MonoBehaviour
{
    public bool startDisplay = true;

    public int numberOfHit = 0;
    public float coolDown = 1.75f;
    [SerializeField] StateManager stateManagerScript;

    private TextMeshProUGUI countUI;


    private void Start()
    {
        countUI = stateManagerScript.comboCounter;
    }

    private void Update()
    {
        if (countUI != null)
            countUI.text = numberOfHit.ToString();
        coolDown -= Time.deltaTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateManagerScript.gettingHit)
        {
            coolDown = 0.75f;
            if (countUI != null)
                countUI.enabled = true;
        }

        else if (coolDown <= 0)
        {
            numberOfHit = 0;
            coolDown = 0.75f;
            if (countUI != null)
                countUI.enabled = false;
        }
    }
}
