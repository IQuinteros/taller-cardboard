using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ganadorUi;
    [SerializeField]
    private GameObject perdedorUi;

    [SerializeField]
    private int puntajeMaximo = 15;
    private int puntajeActual = 0;

    /**
     * <summary>Tiempo en segundos</summary>
     */
    [SerializeField]
    private int tiempoMaximo = 15;

    private bool juegoTerminado = false;
    private bool juegoGanado = false;
    internal Action<int> puntajeActualChanged;
    internal Action<int> tiempoActualChanged;

    public void SumarPuntaje(int puntos)
    {
        puntajeActual += puntos;
        Debug.Log("Puntos: " + puntajeActual + "/" + puntajeMaximo);
        puntajeActualChanged?.Invoke(puntajeActual);
        if(puntajeActual >= puntajeMaximo)
        {
            Debug.Log("Ganaste");
            juegoTerminado = true;
            juegoGanado = true;
            MostrarUI();
            // TODO: Mostrar alerta de ganaste
        }
    }

    protected void ComenzarTiempo()
    {
        StartCoroutine(ContarTiempo());
    }

    protected IEnumerator ContarTiempo()
    {
        int tiempoActual = tiempoMaximo;

        while(tiempoActual > 0)
        {
            if (juegoTerminado)
            {
                Debug.Log("Juego terminado");
                yield break;
            }

            Debug.Log("Tiempo: " + tiempoActual);
            yield return new WaitForSeconds(1);
            tiempoActual--;
            tiempoActualChanged?.Invoke(tiempoActual);
        }
        Debug.Log("Perdiste");
        juegoTerminado = true;
        juegoGanado = false;
        MostrarUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        ComenzarTiempo();
        ganadorUi.SetActive(false);
        perdedorUi.SetActive(false);
        puntajeActualChanged?.Invoke(puntajeActual);
        tiempoActualChanged?.Invoke(tiempoMaximo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MostrarUI()
    {
        if (!juegoTerminado) return;
        if(juegoGanado)
        {
            ganadorUi.SetActive(true);
        } else
        {
            perdedorUi.SetActive(true);
        }
    }

    public int GetPuntajeMaximo()
    {
        return puntajeMaximo;
    }

    public int GetTiempoMaximo()
    {
        return tiempoMaximo;
    }
}
