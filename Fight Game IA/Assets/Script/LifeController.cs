using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Indica que acciones ganan a otras
/// </summary>
public enum Rules
{
    CN = 0,     //<agacharse
    CW = 0,     //<agacharse
    CQ = 0,     //<agacharse
    FN = 0,     //<no hacer nada 
    UN = 0,     //<saltar
    FW = 1,     //<ataque w
    FQ = 2,     //<ataque q
    UW = 3,     //<saltar y ataque w
    UQ = 4,     //<saltar y ataque q (este ataque es vencido con ataque w)
}
public class LifeController : MonoBehaviour
{
    [Header("IA Config")]

    /// <summary>
    /// Indicador de vida de la IA
    /// </summary>
    public int maxIAHealth;

    /// <summary>
    /// Referencia de la IA
    /// </summary>
    public IA machine_controller;

    /// <summary>
    /// Vida actual de la IA
    /// </summary>
    private int currentIAHealth;


    [Header("Player Config")]

    /// <summary>
    /// Indicador de vida del jugador
    /// </summary>
    public int maxPlayerHealth;

    /// <summary>
    /// Referencia del jugador
    /// </summary>
    public CharacterAnimatorController player_controller;

    /// <summary>
    /// Vida actual del player
    /// </summary>
    private int currentPlayerHealth;

    [Header("Daño por accion")]

    /// <summary>
    /// Daño por accion
    /// </summary>
    public int danioAccion;

    /// <summary>
    /// Instancia a si mismo
    /// </summary>
    public static LifeController instance;
    /// <summary>
    /// Los puntos que valen la ultima acción del jugador
    /// </summary>
    private Rules player_points;
    /// <summary>
    /// Los puntos que valen la ultima acción de la ia
    /// </summary>
    private Rules ia_points;

    void Start()
    {
        currentIAHealth = maxIAHealth;
        currentPlayerHealth = maxPlayerHealth;
        instance = this;
        
    }

    // Update is called once per frame
    /// <summary>
    /// Comprueba si alguien ha recibido daño
    /// </summary>
    public void CheckHealt()
    {
        
        string machine_action = machine_controller.Guess();
        string player_action = player_controller.checkAction();
        string prediction_ia = machine_controller.prediction();

        player_points = (Rules)System.Enum.Parse(typeof(Rules), player_action);
        ia_points = (Rules)System.Enum.Parse(typeof(Rules), machine_action);

        
        //Se comprueba cual de los dos ataque es mas fuerte y si alguien ha hecho 4 y el otro 1 que gane el 1 para cerrar el circulo de acciones
        if (player_points > ia_points) { if (player_points == Rules.UQ && ia_points == Rules.FW) { currentPlayerHealth -= danioAccion; } else { currentIAHealth -= danioAccion; } }
        else if (player_points < ia_points) { if (player_points == Rules.FW && ia_points == Rules.UQ) { currentIAHealth -= danioAccion; } else { currentPlayerHealth -= danioAccion; } }

        machine_controller.interactions++;
        Debug.Log("interacciones: " + machine_controller.interactions);
        if (player_action.Equals(prediction_ia))
        {
            machine_controller.correctPredictions++;
        }

    }
    /// <summary>
    /// Retorna la vida del jugador
    /// </summary>
    /// <returns>vida del jugador en enteros</returns>
    public int getPlayerHealth() { return currentPlayerHealth; }
    /// <summary>
    /// Retorna la vida de la IA
    /// </summary>
    /// <returns>vida de la IA en enteros</returns>
    public int getIAHealth() { return currentIAHealth; }
}
