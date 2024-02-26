using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorOfHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Image changeColor;

    [SerializeField] private bool isShake;
    public float positionShakeSpeed = 0.1f;
    public Vector3 positionShakeRange = new Vector3(0.1f, 0.1f, 0.1f);

    private Vector3 position;
    void Start()
    {
        position = transform.localPosition;
    }

    void Update()
    {
        if (slider.value <= 0.6 && slider.value > 0.4)
        {
            changeColor.color= Color.yellow;
        }
        else if (slider.value <= 0.4 && slider.value >= 0.01)
        {
            changeColor.color= Color.red;
            isShake = true;
            StartCoroutine("ShakeThisObject");
        }
        else
        {
            changeColor.color= Color.green;
        }
    }

    IEnumerator ShakeThisObject()
    {
        if(isShake)
        {
           gameObject.transform.localPosition = position + Vector3.Scale(SmoothRandom.GetVector3(positionShakeSpeed), positionShakeRange);
        }

        yield return new WaitForSeconds(0.25f);

        isShake = false;
    }
}
