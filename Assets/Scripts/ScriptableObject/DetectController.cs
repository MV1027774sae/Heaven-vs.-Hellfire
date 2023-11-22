using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ControllerDetect", menuName = "ScriptableObject")]
public class DetectController : ScriptableObject
{
    public bool isConnect = false;
    public bool gameStopped = false;
}
