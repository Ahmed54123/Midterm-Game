using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerSelectMenu; //Player select menu that will be activated and deactivated based on if the round has started or not
    public bool hasGameStarted { get; set; }

    bool _gameOver;
    public bool isGameOver
    {
        get
        {
            return _gameOver;
        }
    }

    public List<GameObject> charactersActive { get; set; } //Every new character spawned will be added to this list. The last character alive will be winner

    //GAME MUSIC
    public AudioSource sourceMusic; //Starts playing  music on the game object when the game starts and stops when the fight is over

    public AudioClip fightMusic; // Music to play when the fighting starts
    public AudioClip characterSelectMusic; //Music when the players are choosing their character

    public GameObject PlayerGameMenu; // Activate the UI for the game including health and player icons
    public GameObject VictoryMenu; //Activate the victory menu only when a player has died
    public TextMeshProUGUI victoryText; //set the winning player's name as the victory text's text

    private static GameManager _instance; // make an instance of this class that can be accessed by all the scripts in the scene
    public static GameManager Instance
    {
        get
        {
            if (_instance is null) Debug.LogError("Game Manager is Null");

            return _instance; //referenced from https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74
        }
    }

    public string Winner { get; set; } //Property the fighter script can reference to display that the other player has won
    void Awake()
    {
        charactersActive = new List<GameObject>();

        sourceMusic = sourceMusic.GetComponent<AudioSource>();
        hasGameStarted = false;
        _gameOver = false;

        VictoryMenu.SetActive(false);
        playerSelectMenu.SetActive(true);

        sourceMusic.clip = fightMusic;
        sourceMusic.Play();
        
        

        _instance = this; // initialize the instance
    }

    // Update is called once per frame
    void Update()
    {
        
        if(hasGameStarted == true)
        {
            

            playerSelectMenu.SetActive(false);
            PlayerGameMenu.SetActive(true);


            
        }

        
    }

    public void GameOver()
    {
        Winner = charactersActive[0].name;
        VictoryMenu.SetActive(true); //set the victory menu to active
        _gameOver = true;                             
        victoryText.text = Winner + " won!"; //set the winner to the winner text game object
        sourceMusic.Stop();
    }

    public void CharacterSpawned(GameObject character)
    {
        //When a character is added to the scene add them to the list
        charactersActive.Add(character); //add this character to the list of characters alive
    }

    public void CharacterDied(GameObject character)
    {
        //When a character dies remove from the list
        charactersActive.Remove(character); //add this character to the list of characters alive
        if(charactersActive.Count< 2)
        {
            GameOver(); //Everytime a character dies, check to make sure how many characters are alive, if there is only one, then they win the game
        }
    }
}
