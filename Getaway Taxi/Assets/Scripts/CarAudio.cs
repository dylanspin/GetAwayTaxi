using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{

    [Header("Random")]
    [SerializeField] private AudioSource bumpSound;
    [SerializeField] private AudioSource bustedSound;
    [SerializeField] private AudioSource intro;

    [Header("Engine")]
    [SerializeField] private AudioClip[] engineSoundClips;
    [SerializeField] private AudioSource engine;

    [Header("Horn")]
    [SerializeField] private AudioClip[] hornClips;
    [SerializeField] private AudioSource hornSound;

    [Header("Private data")]
    private int musicVolume = 50;
    private int mainVolume = 50;
    private bool canHorn = true;

    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = Save.loadSettingData();
        if(loadData != null)//if data was found set to saved data
        {
            musicVolume = loadData.getMusic();
            mainVolume = loadData.getVolume();
        }
        else//if there is no save found
        {
            Debug.Log("No save data found");
        }

        setAudioVolumes();
        playStartAudio();
    }

    private void setAudioVolumes()
    {
        float calcMain = (float)mainVolume/100;
        hornSound.volume = calcMain;
        bumpSound.volume = calcMain;
        engine.volume = calcMain;
        intro.volume = calcMain;
    }

    private void playStartAudio()
    {
        intro.Play();
    }

    private void Update()
    {
        if(canHorn)
        {
            if(OVRInput.GetDown(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.L))
            {
                playHorn();
            }
        }
    }

    private void playHorn()
    {
        canHorn = false;
        AudioClip lastAudio = hornSound.clip;
        AudioClip currentAudio = hornSound.clip;
        if(lastAudio == null)
        {
            currentAudio = hornClips[Random.Range(0,hornClips.Length)];
        }
        else
        {
            while(currentAudio == null || currentAudio == lastAudio)
            {
                currentAudio = hornClips[Random.Range(0,hornClips.Length)];
            }
        }

        hornSound.clip = currentAudio;
        hornSound.Play();
        Invoke("unlockHorn",2);
    }

    private void unlockHorn()
    {
        canHorn = true;
    }

    public void startCar(bool active)
    {
        engine.Stop();
        if(active)
        {
            engine.loop = false;
            engine.clip = engineSoundClips[0];
            engine.Play();
            CancelInvoke("playLoopedEngine");
            Invoke("playLoopedEngine",engineSoundClips[0].length);
        }
    }

    public void playLoopedEngine()
    {
        engine.loop = true;
        engine.clip = engineSoundClips[1];
        engine.Play();
    }

    public void collide(Vector3 posHit)
    {
        bumpSound.transform.position = posHit;
        bumpSound.Play();
    }

    public void playEnd(bool busted)
    {
        if(busted)
        {
            bustedSound.Play();
        }
    }
}
