using System.Collections.Generic;
using UnityEngine;

public class AnnouncerManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public int clipIndex = 0;

    // Update is called once per frame
    void Update()
    {
        clipIndex = Mathf.Clamp(clipIndex, 0, audioClips.Count - 1);
    }
}
