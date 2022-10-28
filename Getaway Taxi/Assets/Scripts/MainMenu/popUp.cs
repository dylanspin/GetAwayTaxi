using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class popUp : MonoBehaviour
{
    [SerializeField] private Animator popUpAnim;

    [Header("Video")]
    [SerializeField] private VideoPlayer video;
    [SerializeField] private AudioController audioController;

    public void setStatePopUp(bool active)
    {
        if(active)
        {
            gameObject.SetActive(active);
            setStartVideo();
        }
        else
        {
            if(video)
            {
                audioController.playMusic(true);
            }
        }
        popUpAnim.SetBool("Show",active);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void disableObject()
    {
        gameObject.SetActive(false);
    }

    private void setStartVideo()
    {
        if(video)
        {
            video.frame = 0;
            video.Play();
            audioController.playMusic(false);
            video.SetDirectAudioVolume(0,audioController.getMainVol());
        }
    }
}
