using System.Collections.Generic;
using UnityEngine;

public class AnnouncerManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public int clipIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clipIndex = Mathf.Clamp(clipIndex, 0, audioClips.Count - 1);
    }
}
