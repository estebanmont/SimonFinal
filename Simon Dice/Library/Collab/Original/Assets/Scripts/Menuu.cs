using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
       /* else if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }*/
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /*public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Principal");
    }

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }*/

}
