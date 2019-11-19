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
    public GamePredictor predictor { get; private set; }

    /// <summary>
    /// Instancia de la IA
    /// </summary>
    public static IA instance;

    /// <summary>
    /// Controlador del jugador
    /// </summary>
    public CharacterAnimatorController PlayerController;

    /// <summary>
    /// Controlador de las animaciones
    /// </summary>
    private Animator animController;

    /// <summary>
    /// Jump component
    /// </summary>
    private Jump jumpComponent;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        predictor = new GamePredictor();
        animController = gameObject.GetComponent<Animator>();
        jumpComponent = gameObject.GetComponent<Jump>();
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
            lastActions = totalActions.Skip(totalActions.Count - windowSize).Take(windowSize).ToList(); // .Substring(totalActions.Length - windowSize, windowSize);
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

        realizarAccion(guess);
        return guess;

    }

    /// <summary>
    /// Funcion de Start
    /// </summary>
    public void Go()
    {

        List<string> player = PlayerController.getPossibleActions();

        foreach (string action in player)
        {
            AddAction(action);
        }

    }

    /// <summary>
    /// Añade una accion a total actions
    /// </summary>
    public void addtotalActions(string action)
    {

        totalActions.Add(action);

        if (totalActions.Count >= windowSize)
        {
            List<string> lastActions = totalActions.Skip(totalActions.Count - windowSize).Take(windowSize).ToList();
            predictor.RegisterActions(lastActions);
        }
    }

    
    /// <summary>
    /// Realizamos la animacion
    /// </summary>
    /// <param name="v"></param>
    private void realizarAccion(string v)
    {
        MakeAnim(v);
    }


    /// <summary>
    /// Hace la animacion
    /// </summary>
    private void MakeAnim(string currentActionString)
    {
        char posicion = currentActionString[0];
        char ataque = currentActionString[1];

        switch (posicion)
        {
            case 'F':
                animController.SetInteger("_animState", 0);
                setAnimAttack(ataque);

                break;

            case 'U':
                animController.SetInteger("_animState", 0);
                jumpComponent.JumpOwner();
                setAnimAttack(ataque);

                break;

            case 'C':
                animController.SetInteger("_animState", 2);
                break;
        }

    }

    private void setAnimAttack(char ataque)
    {
        switch (ataque)
        {
            case 'Q':
                animController.SetInteger("state", 1);
                break;

            case 'W':
                animController.SetInteger("state", 2);
                break;

            default:
                animController.SetInteger("state", 0);
                break;

        }
    }

}

