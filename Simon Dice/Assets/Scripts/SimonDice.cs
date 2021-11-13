using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public enum colores { Azul, Rojo, Amarillo, Verde, Morado, Naranja }

public class SimonDice : MonoBehaviour
{
    private IEnumerator corrutinaTiempo;
    private Queue<colores> Arcoiris = new Queue<colores>();
    private Dificulty settings;
    private int TamañoArcoiris;
    private int PuestoLista=1;
    [SerializeField] GameObject[] teclas;
    [SerializeField] Slider tiempoUI;
    [SerializeField] Text puntajeUI;
   
    private Dictionary<colores, Button> botones= new Dictionary<colores, Button>();
    private bool controlJugador;

    public Dificulty Settings
    {
        private get => settings; set
        { if (settings == null) { settings = value; } }
    }
    private void Awake()
    {
       
    }
    private void Start()
    {
        settings = LevelManager.lvlManager.Dificultad_actual;
        LevelManager.lvlManager.PuntajeJugador = 0;
        int x = 0;
        foreach (GameObject boton in teclas)
        {

            if (x + 1 > settings.cantidadBotones)
            {
                boton.SetActive(false);
            }


            botones.Add((colores)x,boton.GetComponent<Button>());

            x++;

        }
        IniciarJuego();

    }
    private void Update()
    {
        if (controlJugador)
        {
            var teclado = Keyboard.current;

            if (teclado.aKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Azul));
            }
            else if (teclado.dKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Rojo));

            }
            else if (teclado.sKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Amarillo));

            }
            else if (teclado.zKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Verde));

            }
            else if (teclado.cKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Morado));

            }
            else if (teclado.xKey.wasPressedThisFrame)
            {
                StartCoroutine(activarBotonTecla(colores.Naranja));

            }

        }
    }
    public void IniciarJuego()
    {
        GenerarArcoiris();
        StartCoroutine(EnseñarSecuencia());
    }
    public void GenerarArcoiris()
    {
        int nuevocolor = Random.Range(0, settings.cantidadBotones );
        Arcoiris.Enqueue((colores)nuevocolor); //para los que dudan de su cordura, aqui esto es llamado casteo
        TamañoArcoiris++;
        /*foreach (object obj in Arcoiris)
        {
            Debug.Log(obj + " ");
        }
        Debug.Log(".");*/

    }
    public void DesactivarControlJugador()
    {
        controlJugador = false;
        for (int i = 0; i < settings.cantidadBotones; i++)
        {
            botones[(colores)i].interactable = false;
        }
      
    }
    public void ActivarControlJugador()
    {
        controlJugador = true;
        for (int i = 0; i < settings.cantidadBotones; i++)
        {
            botones[(colores)i].interactable = true;
        }
    }
    public void CompararColor(int inputJugador)
    {
        colores siguiente = Arcoiris.Dequeue();
        Arcoiris.Enqueue(siguiente);
        if ((colores)inputJugador != siguiente)
        {
            
            Debug.Log("perdiste manco de mierda");
            LevelManager.lvlManager.LoadScene("GameOver");
            return;
        }
        if (PuestoLista != TamañoArcoiris)
        {
            PuestoLista++;
        }
        else
        {
            int sumaPuntos = (settings.extraPuntos * (int)((TamañoArcoiris-1) / 5)+settings.puntos);
            LevelManager.lvlManager.PuntajeJugador += sumaPuntos;
            puntajeUI.text = LevelManager.lvlManager.PuntajeJugador.ToString();

            PuestoLista = 1;
            StopCoroutine(corrutinaTiempo);
            IniciarJuego();

        }
        
    }

    public IEnumerator EnseñarSecuencia()
    {
        float reducciontasa = (settings.reduccionTasaRepeticion * (int)((TamañoArcoiris-1) / 5));
        float tasarepeticion = settings.tasaRepeticion - reducciontasa;
        //Quitar el input del jugador
        DesactivarControlJugador();
        for (int i = 0; i < TamañoArcoiris; i++)
        {
            colores siguiente = Arcoiris.Dequeue();
            Arcoiris.Enqueue(siguiente);
            var colorboton = botones[siguiente].GetComponent<Image>();
             Color original = colorboton.color;
            yield return new WaitForSeconds(0.4f);
            //hacer que brille el siguiente color
            colorboton.color = Color.gray;
          
            yield return new WaitForSeconds(tasarepeticion);

            //apagar el siguiente color
            colorboton.color = original;
            
            yield return null;
        }
        yield return null;
        //dar input al jugador
       
        ActivarControlJugador();
        corrutinaTiempo = TiempoLimite();
        StartCoroutine(corrutinaTiempo);
    }

    private IEnumerator activarBotonTecla(colores color)
    {
        if (botones[color].interactable) 
        { 

        botones[color].onClick.Invoke();
        var colorboton = botones[color].GetComponent<Image>();
        Color original = colorboton.color;
       
        colorboton.color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        colorboton.color = original;
      
        yield return null;
        }
    }

    private IEnumerator TiempoLimite()
    {
        float tiempo = settings.timeLimit-(settings.reduccionTiempoLimite * (int)((TamañoArcoiris-1) / 5));
        while (tiempo>0)
        {
            tiempo -= Time.deltaTime;
            tiempoUI.value = tiempo;
            yield return null;
        }
        Debug.Log("acaso te dormiste manco de mierda?");
        LevelManager.lvlManager.LoadScene("GameOver");

        yield return null;
    }
}
