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
    private AnimState? _animState;

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
    /// Componente de salto
    /// </summary>
    private Jump jumpComponent;

    /// <summary>
    /// Determina si estoy atacando o no
    /// </summary>
    private bool atacking;

    /// <summary>
    /// Accion actual
    /// </summary>
    public string currentActionString { get; private set; }

    /// <summary>
    /// Siguiente accion si existe
    /// </summary>
    public KeyCode? nextAction { get; private set; }

    /// <summary>
    /// Determina si el componente esta instanciado o no
    /// </summary>
    private bool instanciado = false;

    /// <summary>
    /// Funcion de update
    /// </summary>
    void Update()
    {
        if (instanciado)
        {

            if(Input.GetKeyDown(jump))
            {
                _animState = AnimState.Jump;
            }
            else if(Input.GetKeyDown(crouch))
            {
                _animState = AnimState.Duck;
            }

            foreach (KeyCode key in attacks)
            {
                if (Input.GetKeyDown(key))
                {
                    nextAction = key;
                }
                
            }

        }
    }

    /// <summary>
    /// Hace la animacion
    /// </summary>
    private void MakeAnim()
    {
        char posicion = currentActionString[0];
        char ataque = currentActionString[1];

        switch(posicion)
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


    /// <summary>
    /// Check current action
    /// </summary>
    public string checkAction()
    {
        registerAction(nextAction);
        nextAction = null;
        _animState = AnimState.Floor;
        MakeAnim();
        return currentActionString;

    }

    /// <summary>
    /// Start del objeto
    /// </summary>
    public void Go()
    {

        //Controlador de la animacion
        animController = GetComponent<Animator>();

        if (GetComponent<Jump>())
            jumpComponent = GetComponent<Jump>();
        
        instanciado = true;
    }

    /// <summary>
    /// Devuelve un string de la accion
    /// </summary>
    private string GetAction(KeyCode? attack)
    {
        string action = "";

        if (attack == null)
            attack = KeyCode.None;

        if (_animState == AnimState.Jump)
        {
            action = "U" + attack.ToString().ToCharArray()[0];
        }
        else
        {
            if (_animState == AnimState.Duck)
            {
                action = "C" + attack.ToString().ToCharArray()[0]; ;
            }
            else
            {
                action = "F" + attack.ToString().ToCharArray()[0]; ;
            }

        }

        return action;
    }


    /// <summary>
    /// Registra la accion en la IA
    /// </summary>
    private void registerAction(KeyCode? attack)
    {
        string action = "";

        if (attack == null)
            attack = KeyCode.None;

        if (_animState == AnimState.Jump)
        {
            action = "U" + attack.ToString().ToCharArray()[0];
        }
        else
        {
            if (_animState == AnimState.Duck)
            {
                action = "C" + attack.ToString().ToCharArray()[0]; ;
            }
            else
            {
                action = "F" + attack.ToString().ToCharArray()[0]; ;
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
