using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringObject : MonoBehaviour
{
    public GameObject flickeringObject;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f)
        {
            timer = 0;
            flickeringObject.SetActive(!flickeringObject.activeInHierarchy);
        }
    }
}
