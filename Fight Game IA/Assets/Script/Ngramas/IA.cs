using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    /// <summary>
    /// Tiempo de prediccion
    /// </summary>
    public float tiempoDePrediccion;

    /// <summary>
    /// Contador actual
    /// </summary>
    private float currentTime;

    /// <summary>
    /// Ventana que utilizara para atacar al contrario
    /// </summary>
    public byte windowSize = 2;

    /// <summary>
    /// Total de acciones que puede hacer un jugador
    /// </summary>
    public List<string> totalActions = new List<string>();

    /// <summary>
    /// Posible acciones que puede realizar la IA
    /// </summary>
    private List<string> possibleActions = new List<string>(); 

    /// <summary>
    /// Objeto que predice que hay
    /// </summary>
    private GamePredictor predictor;

    /// <summary>
    /// Instancia de la IA
    /// </summary>
    public static IA instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        predictor = new GamePredictor();
    }

    /// <summary>
    /// Start del script
    /// </summary>
    private void Start()
    {
        List<string> player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAnimatorController>().getPossibleActions();

        foreach (string action in player)
        {
            AddAction(action);
        }

    }

    /// <summary>
    /// Añade una accion a las posibles acciones
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(string action)
    {
        possibleActions.Add(action);
    }

    /// <summary>
    /// Obtiene una accion aleatoria
    /// </summary>
    /// <returns></returns>
    private string RandomGuess()
    {
        return possibleActions[UnityEngine.Random.Range(0, possibleActions.Count)];
    }

    /// <summary>
    /// Intenta adivinar la respuesta correcta
    /// </summary>
    public string Guess()
    {
        List<string> lastActions = new List<string>();
       
        string guess;

        if (totalActions.Count >= windowSize)
        {
            lastActions = totalActions.Skip(totalActions.Count - windowSize).Take(windowSize).ToList();// .Substring(totalActions.Length - windowSize, windowSize);
            guess = predictor.GetMostLikely(lastActions);
            if (guess == " ")
            {
                guess = RandomGuess();
            }
        }
        else
        {
            guess = RandomGuess();
        }


        //lastActions = totalActions.Skip(totalActions.Count - windowSize - 1).Take(windowSize + 1).ToList();
        //predictor.RegisterActions(lastActions);

        Debug.Log(guess);
        return guess;

    }

    /// <summary>
    /// Funcion de update de la IA
    /// </summary>
    private void Update()
    {

        if(currentTime > tiempoDePrediccion)
        {
            currentTime = tiempoDePrediccion;
            realizarAccion(Guess());
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        
    }

    /// <summary>
    /// Realizamos la animacion
    /// </summary>
    /// <param name="v"></param>
    private void realizarAccion(string v)
    {
        throw new NotImplementedException();
    }
}
