using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador de la IA del juego
/// </summary>
public class IA : MonoBehaviour
{

    /// <summary>
    /// Determina si es random o no
    /// </summary>
    public bool random = false;

    /// <summary>
    /// Ventana que utilizara para atacar al contrario
    /// </summary>
    public byte windowSize = 2;

    /// <summary>
    /// Total de acciones que puede hacer un jugador
    /// </summary>
    private List<string> totalActions = new List<string>();

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
    /// <summary>
    /// Accion que va a realizar el contrincante
    /// </summary>
    private string guess;
    /// <summary>
    /// Porcentaje de aciertos de la IA
    /// </summary>
    public int correctPredictions;
    /// <summary>
    /// Cantidad de predicciones que ha hecho la ia
    /// </summary>
    public int interactions;
   

    // Start is called before the first frame update
    void Start()
    {
        interactions = 0;
        correctPredictions = 0;
        instance = this;
        predictor = new GamePredictor();
        animController = gameObject.GetComponent<Animator>();
        jumpComponent = gameObject.GetComponent<Jump>();

        //corregimos la duracion de las animaciones de ataque enfuncion del ritmo del juego
        float duration_animations = 1 / GameManager.instance.pulsoDeAcciones;
        animController.SetFloat("duration_attack1", duration_animations);
        animController.SetFloat("duration_attack2", duration_animations);
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
        string anticipationAction;
       

        List<string> lastActions = new List<string>();


        if (totalActions.Count >= windowSize && !random)
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
        //Teniendo la prediccion del ataque que va ha hacer el contrincante, escoge una opcion mejor
        if ((int)System.Enum.Parse(typeof(Rules), guess) < 4)
            anticipationAction = ((Rules)UnityEngine.Random.Range((int)System.Enum.Parse(typeof(Rules), guess), 4)).ToString();
        else
            anticipationAction = Rules.UQ.ToString();
      
        realizarAccion(anticipationAction);
        return anticipationAction;

    }

    public string prediction()
    {
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

