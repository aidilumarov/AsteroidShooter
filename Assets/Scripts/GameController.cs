using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;

    public Text scoreText;
    public Text gameOverText;
    public Text scoreChangeText;
    public Button restartButton;

    int score;
    bool gameOver;

    // Hazard spawn location
    float xMin, xMax, z;
    int offset = 5;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    Color green = new Color(0, 1, 0);
    Color red = new Color(1, 0, 0);

    void Start()
    {
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        score = 0;
        UpdateScore();

        xMin = ScreenUtils.ScreenLeft + offset;
        xMax = ScreenUtils.ScreenRight - offset;
        z = ScreenUtils.ScreenTop + offset;

        StartCoroutine(SpawnHazardWaves());    
    }

    IEnumerator SpawnHazardWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                var spawnLocation = new Vector3(Random.Range(xMin, xMax), 0, z);

                Instantiate<GameObject>(hazard, spawnLocation, Quaternion.identity);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        if (!gameOver)
        {
            score += scoreToAdd;
            UpdateScore();
            scoreChangeText.color = green;
            scoreChangeText.text = $"+{scoreToAdd}";
        }

    }

    public void ReduceScore(int scoreToReduce)
    {
        if (!gameOver)
        {
            score -= scoreToReduce;
            scoreChangeText.color = red;
            scoreChangeText.text = $"-{scoreToReduce}";

            if (score <= 0)
            {
                score = 0;
            }

            UpdateScore();

            if (score <= 0)
            {
                GameOver();
            }
        }

    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver = true;

        if (score < 100)
        {
            gameOverText.text = "You suck!";
        }

        else
        {
            gameOverText.text = "Not bad!";
        }

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
