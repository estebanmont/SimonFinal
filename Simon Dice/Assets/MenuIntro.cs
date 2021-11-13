using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIntro : MonoBehaviour
{


    public void IniciarJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("simon");
    }
    
    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
