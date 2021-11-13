using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager lvlManager;
    [SerializeField]
    private List<Dificulty> dificultades= new List<Dificulty>();
    public int PuntajeJugador;
    public List<Dificulty> Dificultades { get => dificultades;}
    public Dificulty Dificultad_actual { get => dificultad_actual; private set => dificultad_actual = value; }
    
    private Dificulty dificultad_actual;

    private void Awake()
    {
        if (lvlManager == null)
        {
            lvlManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
    public void StartGame(Dificulty dificultad)//Aqui va a recibir la dificultad
    {
       
        //Aqui se cambia la dificultad
        dificultad_actual = dificultad;
        SceneManager.LoadScene("simon");
       

    }
    public void LoadScene(string nombreescena)
    {
        SceneManager.LoadScene(nombreescena);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
