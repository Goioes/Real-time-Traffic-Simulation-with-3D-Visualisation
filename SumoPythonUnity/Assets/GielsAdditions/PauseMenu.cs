using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public void checkPause() {
        Debug.Log("update frame");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pressed esc");
            if (gameIsPaused)
            {
                Debug.Log("resume");
                resume();
            } else
            {
                Debug.Log("pause");
                pause();
            }
        }
	}

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // normal gamespeed
        gameIsPaused = false;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        gameIsPaused = true;
    }

    public void loadOptions()
    {
        Time.timeScale = 1f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
