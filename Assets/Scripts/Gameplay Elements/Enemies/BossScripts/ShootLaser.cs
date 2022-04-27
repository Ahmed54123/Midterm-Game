using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] ParticleSystem laserStartParticles;
    [SerializeField] ParticleSystem laserEndParticles;

    [SerializeField] int laserAttackDamage;
    public float lineLength  { get;set; } //Set the line length //Enemy 
    [SerializeField] Transform raycastStartPoint;

    bool startParticlesPlaying = false;
    bool endParticlesPlaying = false;
    RaycastHit2D hit;
    public bool isShooting { get; set; }

    [SerializeField] GameObject parentMainGameObject; //make sure the laser isnt colliding with this object
    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        line = GetComponent<LineRenderer>();
        line.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (isShooting == true)
            {
                ShootLasers(laserAttackDamage);
            }


            else if (isShooting == false)
            {
                line.SetPosition(1, new Vector3(lineLength, 0, 0));
                endParticlesPlaying = false;
                laserEndParticles.Stop(true);
                startParticlesPlaying = false;
                laserStartParticles.Stop(true);
                line.enabled = false;
            }
        
    }

    public void ShootLasers(int amountToDamage)
    {
        isShooting = true;

        if (startParticlesPlaying == false)
        {
            startParticlesPlaying = true;
            laserStartParticles.Play(true);
        }

        laserStartParticles.gameObject.transform.position = raycastStartPoint.position;
        line.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint.position, Vector2.up, 300);
        if (hit)
        {
            if (endParticlesPlaying == false)
            {
                endParticlesPlaying = true;
                laserEndParticles.Play(true);
            }

            if(hit.collider.gameObject.GetComponent<iDamageable>() != null && hit.collider.gameObject != parentMainGameObject.gameObject) //make sure the hit isnt on this same game object before taking action
            {
                hit.collider.gameObject.GetComponent<iDamageable>().Damage(amountToDamage);
            }

            laserEndParticles.gameObject.transform.position = hit.point;
            float distance = ((Vector2)hit.point - (Vector2)raycastStartPoint.position).magnitude;
            line.SetPosition(1, new Vector3(distance, 0, 0));
        }

        else
        {
            line.SetPosition(1, new Vector3(250, 0, 0));
            endParticlesPlaying = false;
            laserEndParticles.Stop(true);
        }


    }
}
