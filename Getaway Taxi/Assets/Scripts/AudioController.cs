using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    private int musicVolume;
    private int mainVolume;

    public void setOptions(bool start, int musicSound, int mainSound)
    {
        musicVolume = musicSound;
        mainVolume = mainSound;
        setMusic();

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
    public void setMusic()
    {
        if(musicVolume > mainVolume)
        {
            backgroundMusic.volume = mainVolume;
        }
        else
        {
            backgroundMusic.volume = musicVolume;
        }
    }

    public int getMainVol()
    {
        return mainVolume;
    }
}
