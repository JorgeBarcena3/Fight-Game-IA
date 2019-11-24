using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log("AI: " + machine_action + " -- " + player_action + " :PL");

        if (player_action != machine_action)
        {
            switch (roles(player_action, machine_action))
            {
                case 1:
                    currentIAHealth -= danioAccion;
                    break;
                case 2:
                    currentPlayerHealth -= danioAccion;
                    break;
                case 3:
                    currentIAHealth -= danioAccion;
                    currentPlayerHealth -= danioAccion;
                    break;
            }

        }
    }
    /// <summary>
    /// Determina quien recibe danyo dependiendo de las reglas definidas
    /// </summary>
    /// <param name="player">accion del jugador</param>
    /// <param name="machine">accion de la ia</param>
    /// <returns></returns>
    private int roles(string player , string machine)
    {

        /**
         * 0 ->nadie recibe daño
         * 1 ->Player gana / recibe danyo machine
         * 2 ->Player pierde/ player recibe danyo
         * 3 ->Los dos reciben danyo
         */


        /**
         * player  machine
         * U     - U      -> puede haber colision
         * U     - F      -> UW - !FW gana player
         * U     - C      -> No hay colision
         * F     - F      -> Pude haber colision
         * F     - C      -> no hay colision
         * !N    - N      -> hay colision
         */
        //Conque uno de los dos se agachen nadie recibe daño
        if (player[0] == 'C' || machine[0] == 'C') { return 0; }
        //En el caso de que esten en el mismo lugar, no agachados recibiran los 2 daño
        //porque hacen ataques distintos, sinó no estariamos en esta funcion
        else if (player[0] == machine[0])
        {
            if (player[1] != 'N' && machine[1] != 'N') { return 3; }
            else if (player[1] == 'N') { return 2; }
            else if (machine[1] == 'N') { return 1; }
            else { return 3; }
           
        }
        else
        {
            if (player == "UW" && machine != "FW") { return 1; }
            if (player == "UW" && machine == "FW") { return 0; }
            if (machine == "UW" && player != "FW") { return 2; }
            if (machine == "UW" && player == "FW") { return 0; }

        }
        return 0;

       
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
