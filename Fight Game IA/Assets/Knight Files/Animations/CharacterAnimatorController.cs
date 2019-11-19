using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado de la animacio
/// </summary>
public enum AnimState
{
    Floor = 0,
    Jump = 1,
    Duck = 2

}

/// <summary>
/// Control del personaje
/// </summary>
public class CharacterAnimatorController : MonoBehaviour
{
    /// <summary>
    /// Controlador de las animaciones
    /// </summary>
    private Animator animController;

    /// <summary>
    /// Estado de la animacion
    /// </summary>
    private AnimState _animState;

    /// <summary>
    /// Lista de ataques
    /// </summary>
    public List<KeyCode> attacks;

    /// <summary>
    /// Tecla de salto
    /// </summary>
    public KeyCode jump;

    /// <summary>
    /// Tecla de agacharse
    /// </summary>
    public KeyCode crouch;

    /// <summary>
    /// Tiempo de duracion de ataque
    /// </summary>
    public float timeAtack;

    /// <summary>
    /// Contador
    /// </summary>
    private float auxTime;

    /// <summary>
    /// Componente de salto
    /// </summary>
    private Jump myJumper;

    /// <summary>
    /// Determina si estoy atacando o no
    /// </summary>
    private bool atacking;

    /// <summary>
    /// Contador para el salto
    /// </summary>
    private float auxTimeJump;

    /// <summary>
    /// Accion actual
    /// </summary>
    public string currentActionString { get; private set; }

    /// <summary>
    /// Funcion constructora
    /// </summary>
    void Start()
    {

        //Controlador de la animacion
        animController = GetComponent<Animator>();

        if (GetComponent<Jump>())
            myJumper = GetComponent<Jump>();

        auxTimeJump = 0;
        auxTime = 0;

    }

    /// <summary>
    /// Funcion de update
    /// </summary>
    void Update()
    {

        if (myJumper.highestPoint)
        {
            if (auxTimeJump <= myJumper.timeFloating)
            {
                auxTimeJump += Time.deltaTime;
            }
            else
            {
                myJumper.FallOwner();
                auxTimeJump = 0;
            }

        }
        if (atacking)
        {
            if (auxTime <= timeAtack)
                auxTime += Time.deltaTime;
        }
        else
        {
            auxTime = 0;
        }
        if (auxTime >= timeAtack)
            atacking = false;




        _animState = checkAnimState();
        animController.SetInteger("_animState", (int)_animState);

        //Acciones de movimiento vertical
        if (Input.GetKeyDown(jump)) { myJumper.JumpOwner(); }
        else if (Input.GetKeyDown(crouch)) {/*TODO*/ }
        else { }


        if (!atacking && (myJumper.highestPoint || !myJumper.inAir))
        {
            if (_animState == AnimState.Floor)
            {
                bool estadoInicial = true;

                foreach (KeyCode key in attacks)
                {
                    if (Input.GetKeyDown(key))
                    {
                        animController.SetInteger("state", attacks.IndexOf(key) + 1);
                        atacking = true;
                        registerAction(key);
                        estadoInicial = false;
                    }


                }

                if (estadoInicial) //Volvemos al estado inicial
                {
                    animController.SetInteger("state", 0);
                }
            }
            else if (_animState == AnimState.Duck)
            {
                registerAction(KeyCode.None);
            }
        }

    }

    /// <summary>
    /// Registra la accion en la IA
    /// </summary>
    private void registerAction(KeyCode attack)
    {
        string action = "";

        if (myJumper.highestPoint)
        {
            action = "U" + attack.ToString().ToCharArray()[0];
        }
        else
        {
            if (_animState == AnimState.Floor)
            {
                action = "F" + attack.ToString().ToCharArray()[0]; ;
            }
            else
            {
                action = "C" + attack.ToString().ToCharArray()[0]; ;
            }

        }

        currentActionString = action;
        IA.instance.addtotalActions(action);
    }

    

    private AnimState checkAnimState()
    {

        AnimState current;

        if (Input.GetKeyDown(crouch)) //Agachado
        {
            current = AnimState.Duck;
        }
        else //En el suelo
        {
            current = AnimState.Floor;
        }

        return current;
    }

    /// <summary>
    /// Devuelve todas las posibles acciones del jugador
    /// </summary>
    /// <returns></returns>
    public List<string> getPossibleActions()
    {

        List<string> actions = new List<string>();

        foreach (KeyCode key in attacks)
        {

            char action = key.ToString().ToCharArray()[0];
            actions.Add("U" + action);
            actions.Add("F" + action);
            actions.Add("C" + action);

        }

        return actions;
    }
}
