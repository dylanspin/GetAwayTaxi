using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBustedAnim : MonoBehaviour
{
    public void loadEndScreen()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.01388889f;
        SceneManager.LoadScene(4);//goes to endscreen scene
    }
}
