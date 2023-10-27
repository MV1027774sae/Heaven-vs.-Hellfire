using UnityEngine;
using System.Collections;

[System.Serializable]
public class ButtonRef : MonoBehaviour {

    public GameObject selectIndicator;

    public bool isTurtorial;
    public bool selected;

    void Start()
    {
        selectIndicator.SetActive(false);
    }

    void Update()
    {
        selectIndicator.SetActive(selected);
    }
}
