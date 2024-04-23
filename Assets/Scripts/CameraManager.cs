using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraHolder;

    public List<Transform> players = new List<Transform>();

    private Transform p1;
    private Transform p2;

    private Vector3 middlePoint;

    public float orthoMin = 2;
    public float orthoMax = 6;

    [SerializeField] private float targetZ;
    public float zMin = 2;
    public float zMax = 10;

    private Camera cam;

    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.5f;
    public float slowDownFactor = 0.5f;

    private float originalTimeScale;
    public bool doShake;

    public CameraType cType;

    public enum CameraType
    {
        ortho,
        persp
    }

    void Start()
    {
        cam = Camera.main;
        cameraHolder = cam.transform.parent;
        cType = (cam.orthographic) ? CameraType.ortho : CameraType.persp;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(players[0].position, players[1].position);
        float half = (distance / 2);

        middlePoint = (players[1].position - players[0].position).normalized * half;
        middlePoint += players[0].position;

        switch (cType)
        {
            case CameraType.ortho:
                cam.orthographicSize = 2 * (half/2);

                if (cam.orthographicSize > orthoMax)
                {
                    cam.orthographicSize = orthoMax;
                }

                if (cam.orthographicSize < orthoMin)
                {
                    cam.orthographicSize = orthoMin;
                }

                break;
            case CameraType.persp:
                targetZ = -(2 * (half/2));

                if (Mathf.Abs(targetZ) < Mathf.Abs(zMin))
                {
                    targetZ = -zMin;
                }
                if (Mathf.Abs(targetZ) > Mathf.Abs(zMax))
                {
                    targetZ = -zMax;
                }

                cam.transform.localPosition = new Vector3(0, 1f, targetZ - 0.5f);
                
                break;
        }

        cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, middlePoint, Time.deltaTime * 5);
    }

    public static CameraManager instance;
    public static CameraManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }


    //This function make the camera shake and time maybe slow down
    public void ShakeAndSlowMotion()
    {
        StartCoroutine(ShakeAndSlowMotionCoroutine());
    }

    IEnumerator ShakeAndSlowMotionCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPoint = middlePoint + Random.insideUnitSphere * shakeMagnitude;
            cameraHolder.localPosition = randomPoint;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cameraHolder.localPosition = middlePoint;

        Time.timeScale = slowDownFactor;
        yield return new WaitForSecondsRealtime(shakeDuration * slowDownFactor);

        Time.timeScale = originalTimeScale;
    }
}
