using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public int scoreNumber;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreNumber = Random.Range(0, 9999);
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreNumber.ToString();
    }
}
