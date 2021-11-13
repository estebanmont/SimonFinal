using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menuu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;



    void Update()
    {
        var teclado = Keyboard.current;
        if (teclado.escapeKey.wasPressedThisFrame)
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
        else if (teclado.rKey.wasPressedThisFrame)
        {
           LevelManager.lvlManager.LoadScene("Menu Principal");
        }
       
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

    

}
