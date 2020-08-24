using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Home : MonoBehaviour
{
    GameController gameController;
    MeshRenderer meshRenderer;
    AudioSource damageReceivedAudio;

    public Material homeSafeMaterial;
    public Material homeDangerMaterial;

    int scoreToReduce = 3;

    int cameIn;
    int exited;

    // Start is called before the first frame update
    void Start()
    {
        damageReceivedAudio = GetComponent<AudioSource>();

        meshRenderer = GetComponent<MeshRenderer>();

        gameController = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
    }

    void Update()
    {
        if (cameIn == exited)
        {
            cameIn = 0;
            exited = 0;
            meshRenderer.material = homeSafeMaterial;
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            damageReceivedAudio.Play();

            cameIn += 1;
            gameController.ReduceScore(scoreToReduce);
            
            if (meshRenderer)
            {
                meshRenderer.material = homeDangerMaterial;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            exited += 1;
        }
    }
}
