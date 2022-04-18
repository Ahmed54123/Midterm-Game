using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    // This Class defines functions for various buttons that will be used in the game
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton(string nextSceneToLoad ) ///The scene the start button will load will be assigned in the inspector
    {
        SceneManager.LoadScene(nextSceneToLoad);
        
    }

    public void QuitGame()
    {
        Application.Quit(); //Quit the game if this button is pressed
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Start Screen"); //Return to the start screen if the button is pressed
    }

    public void PlayAnotherRound()
    {
        SceneManager.LoadScene("Loading Screen");
    }
}
