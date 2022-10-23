using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{   
    [Header("Components")]

    [SerializeField] private Slider slider; 
    [SerializeField] private TMPro.TextMeshProUGUI progressText; 
    [SerializeField] private TMPro.TextMeshProUGUI tipText; 

    [Header("Settings")]

    [Tooltip("Random text thats displayed each time scene is loaded")]
    [SerializeField] private string[] tips = {"Some random tip"};
    
    public void Start () 
    { 
        setRandomTip();
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1)); 
    } 

    IEnumerator LoadAsynchronously (int sceneIndex) 
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        while (!operation.isDone) 
        { 
            float progress = Mathf.Clamp01(operation.progress / .9f); 
            
            slider.value = progress; 
            progressText.text = "Going to the garage : " + progress * 100f + "%"; 

            yield return null; 
        } 
    } 

    private void setRandomTip()
    {
        tipText.text = tips[Random.Range(0,tips.Length)];
    }
}
