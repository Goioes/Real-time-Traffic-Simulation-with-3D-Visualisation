using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour {

    /*public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
