using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Posibles estados del juego
/// </summary>
public enum GameState
{
    start = 0,
    playing = 1,
    end = 2
}

/// <summary>
/// Manager del juego
/// </summary>
public class GameManager : MonoBehaviour
{

    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Tiempo de lucha de cada partida
    /// </summary>
    public float gameTime;

    /// <summary>
    /// Contador auxiliar
    /// </summary>
    private float contador;

    /// <summary>
    /// Estado actual del juego
    /// </summary>
    public GameState estadoActual { get; private set; }

    /// <summary>
    /// Cada cuanto se va a llamar al pulso de acciones
    /// </summary>
    public float pulsoDeAcciones;

    /// <summary>
    /// Auxiliar del pulso de acciones
    /// </summary>
    public float aux_pulsoDeAcciones { get; private set; }

    /// <summary>
    /// Controlador de la IA
    /// </summary>
    private IA IaController;

    /// <summary>
    /// Controlador del jugador
    /// </summary>
    private CharacterAnimatorController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        estadoActual = GameState.start;

        IaController = GameObject.FindGameObjectWithTag("IA").GetComponent<IA>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAnimatorController>();

        contador = gameTime;
        aux_pulsoDeAcciones = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(estadoActual == GameState.start)
        {
            starting();
        }
        else if(estadoActual == GameState.playing)
        {
            playing();
        }
        else
        {
            ending();
        }
        
    }

    /// <summary>
    /// Si se ha acabado el juego
    /// </summary>
    private void ending()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Si estamos empezando el juego
    /// </summary>
    private void starting()
    {
        if(Input.anyKeyDown)
        {
            startGame();
        }
    }

    private void startGame()
    {
        estadoActual = GameState.playing;
        contador = gameTime;
        aux_pulsoDeAcciones = 0;

        IaController.Go();
        playerController.Go();


    }

    /// <summary>
    /// Si estamos jugando
    /// </summary>
    private void playing()
    {
        if(contador <= 0)
        {
            estadoActual = GameState.end;
        }
        else
        {
            contador -= Time.deltaTime;
        }

        //Pulso de acciones
        if(aux_pulsoDeAcciones >= pulsoDeAcciones)
        {
            aux_pulsoDeAcciones = 0;
            LifeController.instance.CheckHealt();

        }
        else
        {
            aux_pulsoDeAcciones += Time.deltaTime;
        }


    }

    /// <summary>
    /// Devuelve el tiempo que queda formateado
    /// </summary>
    /// <returns></returns>
    public string getFormatTime()
    {
        TimeSpan ts = TimeSpan.FromSeconds(contador);
        return ts.ToString(@"mm\:ss");

    }
}
