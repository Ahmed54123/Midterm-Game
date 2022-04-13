using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    PlayerInputManager playerInputManagerRef; // reference to player input manager to keep track of player count

    //make a list of prefabs of fighters
   public List<GameObject> fighters = new List<GameObject>();

    //property that can be changed to decide what character the player wants to use
    public int FighterChosenByPlayer { get; set; }

    public TextMeshProUGUI headertext; //Set the header text to the player currently selecting their character
    bool hasAfighterBeenChosen = false; // Make sure the player has selected a fighter before joining the game

    public GameObject [] healthbar = new GameObject[2]; //Healthbar will be set active when player is spawned
    
    void Start()
    {
        playerInputManagerRef = GetComponent<PlayerInputManager>();
        playerInputManagerRef.DisableJoining(); //disbale joining until the player has picked a fighter
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets each subsequent player to be their chosen character      
        playerInputManagerRef.playerPrefab = fighters[FighterChosenByPlayer];

        headertext.text = "Player" + " " + (playerInputManagerRef.playerCount + 1).ToString(); //Set the header text to whatever the next player's number is going to be;
        
        if(playerInputManagerRef.playerCount >= 2)
        {
            GameManager.Instance.hasGameStarted = true; //If the game has two players, start the game
        }
    }

    public void ChooseFighter(int fighter)
    {
        FighterChosenByPlayer = fighter-1; //the button the player has clicked will have a number assigned which will correspond to the fighter's position in the list, selecting it to be the player's fighter
        hasAfighterBeenChosen = true;
    }

    public void JoinPlayer()
    {
        if (hasAfighterBeenChosen == true)
        {
            playerInputManagerRef.EnableJoining();
            

            
            
        }
      
        
        
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        healthbar[playerInputManagerRef.playerCount - 1].gameObject.SetActive(true); //Set the healthbar of the current player to active
        hasAfighterBeenChosen = false; // reset the fighter has been chosen
        playerInputManagerRef.DisableJoining(); //disable joining again until a fighter has been chosen
    }

}
