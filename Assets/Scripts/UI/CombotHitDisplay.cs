using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombotHitDisplay : MonoBehaviour
{
    public bool startDisplay = true;

    [SerializeField] int numberOfHit = 0;
    [SerializeField] float coolDown = 1.5f;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;
        if (stateManagerScript.gettingHit == true)
        {
            numberOfHit += 1;
            coolDown = 1.5f;
            if (countUI != null)
                countUI.enabled = true;
        }

        else if (coolDown <= 0)
        {
            numberOfHit = 0;
            coolDown = 1.5f;
            if (countUI != null)
                countUI.enabled = false;
        }
    }
}
