using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject explosionPrefab_player;

    GameController gameController;

    int increaseScore = 1;

    void Start()
    {
        gameController = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary") ||
            other.gameObject.CompareTag("Home"))
        {
            return;
        }

        if (other.gameObject.CompareTag("LaserBolt"))
        {
            Instantiate<GameObject>(explosionPrefab, transform.position, transform.rotation);
            gameController.AddScore(increaseScore);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate<GameObject>(explosionPrefab_player, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        Destroy(other.gameObject);
        Destroy(gameObject);

    }
}
