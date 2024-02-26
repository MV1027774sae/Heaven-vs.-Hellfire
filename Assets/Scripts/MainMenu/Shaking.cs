using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField] bool shaking;
    [SerializeField] float shakeValue;
    [SerializeField] Vector3 shakePosition;

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shakeValue);
            //This two line is making sure that the Y and Z won't move during the shake. Remove or comment it if you want to move in other direction.
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    public void ShakeMe()
    {
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow()
    {
        if (shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.2f);

        shaking = false;
        transform.position = shakePosition;
    }
}
