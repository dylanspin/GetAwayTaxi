using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{

    [Header("Random")]
    [SerializeField] private AudioSource bumpSound;//the bumping in to stuff effect audio source where the sound comes from
    [SerializeField] private AudioSource bustedSound;//the caught/lost end audio source where the sound comes from
    [SerializeField] private AudioSource intro;//the intro voice over audio source where the sound comes from

    [Header("Music")]
    [SerializeField] private AudioClip[] musicTracks;
    [SerializeField] private AudioSource musicSource;//the in game car music audio source where the sound comes from

    [Header("Engine")]
    [SerializeField] private AudioClip[] engineSoundClips;
    [SerializeField] private AudioSource engine;//the Engine audio source where the sound comes from

    [Header("Horn")]
    [SerializeField] private AudioClip[] hornClips;
    [SerializeField] private AudioSource hornSound;//the horn audio source where the sound comes from

    [Header("Private data")]
    private int musicVolume = 50;//the amount for the music volume is between 0 - 100
    private int mainVolume = 50;//the amount for the main volume is between 0 - 100
    private bool canHorn = true;//To add a delay between the times people can horn the car

    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = Save.loadSettingData();//gets the setting data
        if(loadData != null)//if data was found set to saved data
        {
            musicVolume = loadData.getMusic();//sets the values from the save data
            mainVolume = loadData.getVolume();//sets the values from the save data
        }
        else//if there is no save found
        {
            Debug.Log("No save data found");
        }

        setAudioVolumes();//sets the volume of the audio sources
        playStartAudio();//plays starting Audio with the voice over
    }

    private void setAudioVolumes()
    {
        float calcMain = (float)mainVolume/100;//The volume of sources can be between 1-0 this conferts it from 100-0 to 1-0
        float calcMusic = (float)musicVolume/100;

        if(calcMusic > calcMain)//if music volume is louder then overall volume
        {
            calcMusic = calcMain;
        }

        //sets the volumes of the audio sources with the main volume
        hornSound.volume = calcMain;
        bumpSound.volume = calcMain;
        engine.volume = calcMain *= 0.75f;
        intro.volume = calcMain;
        
        //sets the volumes of the audio sources with the music volume
        musicSource.volume = calcMusic *= 0.65f;
    }

    private void playStartAudio()//plays short starting instruction voice over
    {
        intro.Play();
        Invoke("playMusic",intro.clip.length);//starts playing in game car music after the intro is done
    }

    private void Update()
    {
        if(canHorn)
        {
            if(OVRInput.GetDown(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.L))//checks if the car horn input is pressed
            {
                playHorn();//horns the car
            }
        }
    }

    private void playHorn()//plays the car horn sound 
    {
        canHorn = false;//sets the delay bool so cant spam the horn with overlapping sounds
        AudioClip lastAudio = hornSound.clip;//the last horn sound effect clip
        AudioClip currentAudio = hornSound.clip;//the current one to be set to a new random one that cant be the same as the last audio clip
        if(lastAudio == null)//if the clip has not been set it doesnt need to check if its the same as the last clip so it sets it to one randomly from all the clips
        {
            currentAudio = hornClips[Random.Range(0,hornClips.Length)];//sets clip to random horn audio clip
        }
        else//if already has audio clip set new one that cant be the same
        {
            while(currentAudio == null || currentAudio == lastAudio)//makes sure the new audio clip cant be the same as the one before
            {
                currentAudio = hornClips[Random.Range(0,hornClips.Length)];//sets clip to random horn audio clip
            }
        }

        hornSound.clip = currentAudio;//sets audiosouces clip to the new horn sound effect clip
        hornSound.Play();//plays the horn sound effect
        Invoke("unlockHorn",2);//disables the delay on honking the car over 2 seconds
    }

    private void unlockHorn()//is called from invoke and allows the car to be honked again
    {
        canHorn = true;
    }

    private void playMusic()//plays next song that cant be the same as the one before
    {
        AudioClip currentAudio = null;
        if(musicSource.clip == null)//if there is no clip said just said to one random music audio clip
        {
            currentAudio = musicTracks[Random.Range(0,musicTracks.Length)];//picks random music audio clip
        }
        else//if there is already a clip set sets to new one that cant be the same as the one before
        {
            AudioClip lastAudio = musicSource.clip;//the last audio music clip
            while(currentAudio == null || currentAudio == lastAudio)//makes sure it cant be the same clip as before
            {
                currentAudio = musicTracks[Random.Range(0,musicTracks.Length)];//picks random music audio clip
            }
        }

        musicSource.clip = currentAudio;//sets the music audio sources clip to the new random clip
        musicSource.Play();//plays the music

        Invoke("playMusic",currentAudio.length);//call the switch to the next music after the time this audio clips takes
    }

    public void startCar(bool active)//plays the start car engine sound effect
    {
        engine.Stop();
        CancelInvoke("playLoopedEngine");//makes sure there arent any double invokes called
        if(active)
        {
            engine.loop = false;//disables the looping so it wont loop the start up engine sound
            engine.clip = engineSoundClips[0];//sets the audio source to the starting engine sound clip
            engine.Play();//plays engine sound effect
            Invoke("playLoopedEngine",engineSoundClips[0].length);//calls the function that sets the looping engine sound effect after the amount of 
        }
    }

    public void playLoopedEngine()//plays looping engine sound effect
    {
        engine.loop = true;//sets source back to looping so the sound effect of the engine doesnt stop
        engine.clip = engineSoundClips[1];//sets the audio clip to the looped engine sound clip
        engine.Play();//plays the engine audio source again
    }

    public void collide(Vector3 posHit)//when collided play sound effect at position of collision
    {
        bumpSound.transform.position = posHit;//sets the collision audio source to the position of the collision
        bumpSound.Play();//plays the crash sound effect
    }

    public void playEnd(bool busted)//plays the end 
    {
        if(busted)
        {
            bustedSound.Play();//plays busted/caught sound effect
        }
        else
        {
            //if we had a winning sound effect
        }
    }
}
