using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    /// <summary>
    /// Ventana que utilizara para atacar al contrario
    /// </summary>
    public byte windowSize = 2;

    /// <summary>
    /// Total de acciones que puede hacer un jugador
    /// </summary>
    public string totalActions = "";

    /// <summary>
    /// Posible acciones que puede realizar la IA
    /// </summary>
    private string possibleActions = ""; 

    /// <summary>
    /// Objeto que predice que hay
    /// </summary>
    private GamePredictor predictor;

    /// <summary>
    /// Instancia de la IA
    /// </summary>
    public static IA instance;

    /// <summary>
    /// Porcentaje de aciertos y fallos
    /// </summary>
    public float acierto = 0, total = 0;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        predictor = new GamePredictor();
    }

    /// <summary>
    /// Añade una accion a las posibles acciones
    /// </summary>
    /// <param name="action"></param>
    public void AddAction(char action)
    {
        possibleActions += action;
    }

    /// <summary>
    /// Obtiene una accion aleatoria
    /// </summary>
    /// <returns></returns>
    private char RandomGuess()
    {
        return possibleActions[Random.Range(0, possibleActions.Length)];
    }

    /// <summary>
    /// Intenta adivinar la respuesta correcta
    /// </summary>
    /// <param name="correctAnswer"></param>
    public void Guess(char correctAnswer)
    {
        string lastActions = "";
        string frase = "";
        total++;
        char guess;
        if (totalActions.Length >= windowSize)
        {
            lastActions = totalActions.Substring(totalActions.Length - windowSize, windowSize);
            guess = predictor.GetMostLikely(lastActions);
            if (guess == ' ')
            {
                guess = RandomGuess();
            }
        }
        else
        {
            guess = RandomGuess();
        }

        if (guess == correctAnswer)
        {
            acierto++;
            frase += "ACIERTO";
        }
        else
        {
            frase += "FALLO";
        }
        frase += " Tasa de aciertos: " + acierto / total + " \n";


      
        lastActions = totalActions.Substring(totalActions.Length - windowSize - 1, windowSize + 1);
        predictor.RegisterActions(lastActions);

    }
    
}
