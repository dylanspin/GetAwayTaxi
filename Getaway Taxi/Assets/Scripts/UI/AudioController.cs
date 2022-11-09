using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;//the audio source where the sound of the background music comes from
    [SerializeField] private AudioSource buttonEffect;//the audio source where the sound of the button sound effect comes from

    private int musicVolume;//the value for the music volume
    private int mainVolume;//the value for the main over all volume

    public void setOptions(bool start, int musicSound, int mainSound)//sets the value from the options
    {
        musicVolume = musicSound;
        mainVolume = mainSound;
        setSound();//sets the audio sources with the settings

        if(start)
        {
            backgroundMusic.Play();//plays background music
        }
    }

    public void playMusic(bool active)//turn on or of the background music
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
    
    public void setSound()//sets the volume of the sources with the values of the options
    {
        float mainVolumeCal = (float)mainVolume/100;
        buttonEffect.volume = mainVolumeCal;//sets the volume

        if(musicVolume > mainVolume)//if the music volume is more then the main volume set it to the music volume
        {
            backgroundMusic.volume = mainVolumeCal;
        }
        else
        {
            backgroundMusic.volume = (float)musicVolume/100;//sets the volume with the music volume
        }
    }

    public void playButtonEffect()//plays button sound effect on hover
    {
        buttonEffect.Play();
    }

    public int getMainVol()//returns the mainvolume value
    {
        return mainVolume;
    }
}
