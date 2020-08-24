using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ship
    public float speed;
    public float tilt;

    // Shot
    public GameObject shotPrefab;
    public Transform shotSpawnTransform;
    public float fireRate = 0.5f;
    private float nextFire = 0f;

    // Switching engine glow color
    public Material engineGlowBlue;
    public Material engineGlowRed;


    // Save for optimization purposes
    Rigidbody rbd;
    List<ParticleSystemRenderer> engineGlows;

    // Clamping
    float xMin, xMax, zMin, zMax;
    float offset = 5;

    // Laser shot audio
    AudioSource laserShotSound;

    // Touch joystick movement
    public Joystick joystick;

    private void Start()
    {
        laserShotSound = GetComponent<AudioSource>();

        xMin = ScreenUtils.ScreenLeft + offset;
        xMax = ScreenUtils.ScreenRight - offset;
        zMin = ScreenUtils.ScreenBottom + offset;
        zMax = ScreenUtils.ScreenTop - offset;

        rbd = GetComponent<Rigidbody>();
        
        var engineGlowObjects = GameObject.FindGameObjectsWithTag("EngineGlow");
        engineGlows = new List<ParticleSystemRenderer>();

        foreach (var ego in engineGlowObjects)
        {
            engineGlows.Add(ego.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>());
        }
    }
    void FixedUpdate()
    {
        // Moves the ship
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        if (rbd)
        {
            rbd.velocity = movement * speed;
        }

        // If there is ship movement, switch engine glow material to red
        if (rbd)
        {
            if (rbd.velocity.x != 0 ||
                rbd.velocity.y != 0 ||
                rbd.velocity.z != 0)
            {
                foreach (var engineGlow in engineGlows)
                {
                    engineGlow.material = engineGlowRed;
                }
            }

            else
            {
                foreach (var engineGlow in engineGlows)
                {
                    engineGlow.material = engineGlowBlue;
                }
            }
        }


        // Clamps the ship within screen area
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(rbd.position.x, xMin, xMax),
            0f,
            Mathf.Clamp(rbd.position.z, zMin, zMax)
            );

        rbd.position = clampedPosition;

        // Tilt the ship when moving
        Quaternion zRotation = Quaternion.Euler(0f, 0f, rbd.velocity.x * -tilt);
        rbd.rotation = zRotation;
    }

    public void Shoot()
    {
        if (nextFire <= Time.time)
        {
            nextFire = Time.time + fireRate;

            Instantiate<GameObject>(shotPrefab,
                shotSpawnTransform.position,
                shotSpawnTransform.rotation); ;

            if (laserShotSound) laserShotSound.Play();
        }
    }
}
