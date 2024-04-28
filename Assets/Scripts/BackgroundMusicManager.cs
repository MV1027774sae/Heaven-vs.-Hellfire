using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource source;

    public AudioClip[] musicList;

    void Start()
    {
        source = GetComponent<AudioSource>();
        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        source.clip = (musicList[Random.Range(0, musicList.Length)]);
        source.Play();
    }
}
