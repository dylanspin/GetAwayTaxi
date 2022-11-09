using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class popUp : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator popUpAnim;//the animator for the pop up effect

    [Header("Video")]
    [SerializeField] private VideoPlayer video;//the video player on the pop up
    [SerializeField] private AudioController audioController;//the audio controller for the scene

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

    public void exitGame()//quit the whole game
    {
        Application.Quit();
    }

    private void setStartVideo()//checks if this is a video pop up if so play the video 
    {
        if(video)
        {
            video.frame = 0;
            video.Play();
            audioController.playMusic(false);//turn of the background music so it doesnt overlap with the video audio
            video.SetDirectAudioVolume(0,audioController.getMainVol());//sets the volume of the video player
        }
    }
}
