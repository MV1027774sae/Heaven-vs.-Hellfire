using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] attackSFX;
    public AudioClip[] lightHitSFX;
    public AudioClip[] heavyHitSFX;
    public AudioClip[] gruntSFX;
    public AudioClip[] fireballCastSFX;
    public AudioClip[] introVoiceLine;

    public void PlayAttackSFX(int index)
    {
        source.PlayOneShot(attackSFX[index]);
    }

    public void PlayLightHitSFX()
    {
        source.PlayOneShot(lightHitSFX[Random.Range(0, lightHitSFX.Length)]);
    }

    public void PlayHeavyHitSFX()
    {
        source.PlayOneShot(heavyHitSFX[Random.Range(0, heavyHitSFX.Length)]);
    }

    public void PlayFireballCastSFX()
    {
        source.PlayOneShot(fireballCastSFX[0]);
    }

    public void PlayIntroVoice()
    {
        source.PlayOneShot(introVoiceLine[Random.Range(0, introVoiceLine.Length)]);
    }
}
