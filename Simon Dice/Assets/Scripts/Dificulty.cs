using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="nueva dificultad", menuName = "crear dificultad")]
public class Dificulty : ScriptableObject
{
    public string dificultad;
    public int cantidadBotones;
    public float timeLimit;
    public float tasaRepeticion;
    public float reduccionTiempoLimite;
    public float reduccionTasaRepeticion;
    public int puntos;
    public int extraPuntos;
}
