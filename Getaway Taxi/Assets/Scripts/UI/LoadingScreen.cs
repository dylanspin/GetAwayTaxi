using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{   
    [Header("Components")]

    [SerializeField] private Slider slider; //the slider for the loading screen to show the progres of the loading screen
    [SerializeField] private TMPro.TextMeshProUGUI progressText; //the text object to display the value of the loading progress
    [SerializeField] private TMPro.TextMeshProUGUI tipText; //the text for loading a random tip

    [Header("Settings")]

    [Tooltip("Random text thats displayed each time scene is loaded")]
    [SerializeField] private string[] tips = {"Some random tip"};//rnndom tips string arrays
    
    public void Start () 
    { 
        setRandomTip();//set the random tip text
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1)); //starts the async loading for the next scene 
    } 

    IEnumerator LoadAsynchronously (int sceneIndex) 
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); //starts the async loading for the next scene 

        while (!operation.isDone) 
        { 
            float progress = Mathf.Clamp01(operation.progress / .9f); 
            
            slider.value = progress; //sets the loading progress value on the slider
            progressText.text = "Going to the garage : " + progress * 100f + "%"; //displays the value of the loading progress

            yield return null; 
        } 
    } 

    private void setRandomTip()
    {
        tipText.text = tips[Random.Range(0,tips.Length)];//set text to random string of array
    }
}
