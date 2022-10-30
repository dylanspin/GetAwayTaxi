using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource buttonEffect;

    private int musicVolume;
    private int mainVolume;

    public void setOptions(bool start, int musicSound, int mainSound)
    {
        musicVolume = musicSound;
        mainVolume = mainSound;
        setSound();

        if(start)
        {
            backgroundMusic.Play();
        }
    }

    public void playMusic(bool active)
    {
        if(active)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Stop();
        }
    }
    public void setSound()
    {
        float mainVolumeCal = (float)mainVolume/100;
        buttonEffect.volume = mainVolumeCal;

        if(musicVolume > mainVolume)
        {
            backgroundMusic.volume = mainVolumeCal;
        }
        else
        {
            backgroundMusic.volume = (float)musicVolume/100;
        }
    }

    public void playButtonEffect()
    {
        buttonEffect.Play();
    }

    public int getMainVol()
    {
        return mainVolume;
    }
}
