using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, iDamageable
{
    public int _maxHealth;
    public int maxHealth { get { return _maxHealth; } }
    public int Health { get; set; } //Declare health variable for iDamageable

    Animator healthPickupAnimator;

    //Health Restoration Variables
    public float healTimeRate; //The time rate that the health will be restored over
    public int healRate; //Amount of health restored over the time rate

    //Variables when player is in the health field
    bool isThisPodTaken; //make sure another player isn't already in the pod

    bool playerHealthFilled; //Check whether the player's health was restored or this object was destroyed

    List<GameObject> playersInHealthPod = new List<GameObject>(); //get a list of all the players in the pod, if there is more than one, push the second player out
    [SerializeField] Transform playerKickoutArea;

    bool entryAnimation = false;
    void Start()
    {
        healthPickupAnimator = gameObject.GetComponent<Animator>();
        isThisPodTaken = false; 
        playerHealthFilled = false;
        Health = maxHealth;

        foreach(CapsuleCollider2D cc2d in gameObject.GetComponents<CapsuleCollider2D>()) //Get each capsule collider component and set it to false until the player enters the field
        {
            cc2d.enabled = false;
        }

    }

    void Update()
    {

        if(isThisPodTaken == true)
        {
            //animation to close the pod
        }

        if (Health <= 0)
        {
            Die();
        }


    }

    public void Damage(int damageTaken)
    {
        Health = Mathf.Clamp(Health - damageTaken, 0, maxHealth);

    }

    public void Die()
    {
        EventManager.Instance.CanStartNewEvent(); //End the event to reset all variables

      
        if (playerHealthFilled == false)
        {
            //Explosion Animation 
            healthPickupAnimator.SetTrigger("explode");
            Debug.Log("Exploded Health pod");
        }

        else 
        {
            //Dismantle Animation
            healthPickupAnimator.SetTrigger("dismantle");
            Debug.Log("Dismantled health pod");

            
           
        }

        destroyGameObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {

        if (entryAnimation == true)
        {

            FighterScript playerRef = collision.gameObject.GetComponent<FighterScript>();
            if (playerRef != null)
            {
                playersInHealthPod.Add(collision.gameObject);

                if (playersInHealthPod.Count >= 2)
                {
                    collision.gameObject.transform.position = playerKickoutArea.transform.position;
                }

                else if (playersInHealthPod.Count < 2)
                {
                    healthPickupAnimator.SetTrigger("Fill Health");
                    isThisPodTaken = true;
                    foreach (CapsuleCollider2D cc2d in gameObject.GetComponents<CapsuleCollider2D>()) //Get each capsule collider component and set it to true when the player enters the field
                    {
                        cc2d.enabled = true;
                    }
                }

                StartCoroutine(AddHealth(collision.gameObject.GetComponent<FighterScript>()));


            }
        }
    }

    IEnumerator AddHealth(FighterScript playerHealth)
    {
        while (true)
        { // loops forever...
            if (playerHealth.Health < playerHealth.maxHealth)
            { // if health < 100...
                playerHealth.Heal(healRate); // increase health and wait the specified time

              

                yield return new WaitForSeconds(healTimeRate);
            }
            else
            { // if health >= 100, just yield 
                playerHealthFilled = true;
                Die();
                yield return null;
            }
        }
    }

    public void EntryOver()
    {
        entryAnimation = true;
    }

    public void destroyGameObject()
    {
        Destroy(gameObject);
    }
}
 

