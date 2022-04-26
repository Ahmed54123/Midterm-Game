using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] ParticleSystem laserStartParticles;
    [SerializeField] ParticleSystem laserEndParticles;
    [SerializeField] float lineLength = 10f;

    bool startParticlesPlaying = false;
    bool endParticlesPlaying = false;
    RaycastHit2D hit;
    public bool isShooting { get; set; }
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
        if(isShooting == true)
        {
            if (isShooting == true)
            {
                ShootLasers();
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
    }

    public void ShootLasers()
    {
        isShooting = true;

        if (startParticlesPlaying == false)
        {
            startParticlesPlaying = true;
            laserStartParticles.Play(true);
        }

        laserStartParticles.gameObject.transform.position = transform.position;
        line.enabled = true;

        hit = Physics2D.Raycast(transform.position, Vector2.right, lineLength);
        if (hit)
        {
            if (endParticlesPlaying == false)
            {
                endParticlesPlaying = true;
                laserEndParticles.Play(true);
            }

            laserEndParticles.gameObject.transform.position = hit.point;
            float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
            line.SetPosition(1, new Vector3(distance, 0, 0));
        }

        else
        {
            line.SetPosition(1, new Vector3(lineLength, 0, 0));
            endParticlesPlaying = false;
            laserEndParticles.Stop(true);
        }


    }
}
