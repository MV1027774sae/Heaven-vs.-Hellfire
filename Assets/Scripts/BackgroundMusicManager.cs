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
        source.PlayOneShot(musicList[Random.Range(0, musicList.Length)]);
    }
}
