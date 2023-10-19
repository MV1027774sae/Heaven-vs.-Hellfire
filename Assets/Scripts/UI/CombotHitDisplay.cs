using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombotHitDisplay : MonoBehaviour
{
    [SerializeField] int numberOfHit = 0;
    [SerializeField] float coolDown = 1.5f;
    public TextMeshProUGUI countUI;

    [SerializeField] StateManager stateManagerScript;

    private void Start()
    {
        countUI = stateManagerScript.comboCounter;
    }

    private void Update()
    {
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
            StartCoroutine(ResetBoolean());
        }

        else if (coolDown <= 0)
        {
            numberOfHit = 0;
            coolDown = 1.5f;
        }
    }

    private IEnumerator ResetBoolean()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
