﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level0");
    }

    public void QuitGame()
    {
        Debug.Log("Application quitting...");
        Application.Quit();
    }
}
