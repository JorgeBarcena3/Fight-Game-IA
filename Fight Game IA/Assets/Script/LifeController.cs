﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
   /// <summary>
   /// Referencia del jugador
   /// </summary>
    public GameObject player;
    /// <summary>
    /// Referencia de la IA
    /// </summary>
    public GameObject machine;
    /// <summary>
    /// Acceso al script que controla al jugador
    /// </summary>
    private CharacterAnimatorController player_controller;
    /// <summary>
    /// Acceso al script que controla a la IA
    /// </summary>
    private IA machine_controller;
    /// <summary>
    /// Indicador de vida del jugador
    /// </summary>
    private int player_healt;
    /// <summary>
    /// Indicador de vida de la IA
    /// </summary>
    private int machine_healt;
    void Start()
    {
        player_healt = 100;
        machine_healt = 100;
        
    }

    // Update is called once per frame
   /// <summary>
   /// Comprueba si alguien ha recibido daño
   /// </summary>
    private void CheckHealt()
    {
        string player_action = player_controller.currentActionString;
        string machine_action = machine_controller.totalActions[machine_controller.totalActions.Count];
        if (player_action != machine_action)
        {
            switch (roles(player_action, machine_action))
            {
                case 1:
                    machine_healt -= 10;
                    break;
                case 2:
                    player_healt -= 10;
                    break;
                case 3:
                    machine_healt -= 10;
                    player_healt -= 10;
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
         */
        //Conque uno de los dos se agachen nadie recibe daño
        if (player[0] == 'C' || machine[0] == 'C') { return 0; }
        //En el caso de que esten en el mismo lugar, no agachados recibiran los 2 daño
        //porque hacen ataques distintos, sinó no estariamos en esta funcion
        else if (player[0] == machine[0]) { return 3; }
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
    public int GetPlayerHealt() { return player_healt; }
    /// <summary>
    /// Retorna la vida de la IA
    /// </summary>
    /// <returns>vida de la IA en enteros</returns>
    public int GetMachineHealt() { return machine_healt; }
}
