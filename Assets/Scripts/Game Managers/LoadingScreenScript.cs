using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    //HINT VARIABLES
    public TextMeshProUGUI hint; //The hint text which will display the random hint provided;
    public string[] hints; //An array of hints that will be chosen at random every time this script is called

    //IMAGE VARIABLES
    public GameObject loadingImageMain; //The image to be altered in the canvas
    public Sprite[] splash_images; //splash screen images that can be inputted by the player 
    
    

    //Load next scene variables
    public int SecondsToWait; // Seconds to wait before loading the next scene
    public string whatSceneToLoad; //input a scene to load next
    void Start()
    {
        
        loadingImageMain.gameObject.GetComponent<Image>().sprite = splash_images[Random.Range(0, splash_images.Length)]; //Get the image component of the game object then Select a random sprite as the loading splash image
        hint.text = "Hint: " + hints[Random.Range(0, hints.Length - 1)]; // Select a random hint to display 
        StartCoroutine(WaitBeforeLoadingNextScene(SecondsToWait, whatSceneToLoad));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitBeforeLoadingNextScene(int secondsToWait, string sceneToLoad)
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsToWait); // Wait this amount of time before loading the next scene
            SceneManager.LoadScene(sceneToLoad); //Load The inputted scene
        }
    }
}
