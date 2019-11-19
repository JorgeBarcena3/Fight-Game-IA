using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimState
{
    Floor = 0,
    Jump = 1,
    Duck = 2

}

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


    public KeyCode atack1;
    public KeyCode atack2;
    public KeyCode atack3;
    public KeyCode atack4;
    public KeyCode jump;
    public KeyCode crouch;
    public float timeAtack;
    private float auxTime;
    private Jump myJumper;
    private bool atacking;
    private float auxTimeJump;

    // Start is called before the first frame update
    void Start()
    {
        //Controlador de la animacion
        animController = GetComponent<Animator>();

        if (GetComponent<Jump>())
            myJumper = GetComponent<Jump>();

        auxTimeJump = 0;
        auxTime = 0;

    }

    // Update is called once per frame
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
                if (Input.GetKeyDown(atack1)) //Ataque 1
                {
                    animController.SetInteger("state", 1);
                    atacking = true;
                    registerAction(atack1);
                }
                else if (Input.GetKeyDown(atack2)) //Ataque 2
                {
                    animController.SetInteger("state", 2);
                    atacking = true;
                    registerAction(atack2);

                }
                else //Volvemos al estado inicial
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
        string position = "";

        if (myJumper.highestPoint)
        { 
            position = "U" + attack.ToString().ToCharArray()[0];
        }
        else
        {
            if (_animState == AnimState.Floor)
            {
                position = "F" + attack.ToString().ToCharArray()[0]; ;
            }
            else
            {
                position = "C" + attack.ToString().ToCharArray()[0]; ;
            }

        }

        IA.instance.addtotalActions(position);
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

        if (atack1 != KeyCode.None)
        {
            char action = atack1.ToString().ToCharArray()[0];
            actions.Add("U" + action);
            actions.Add("F" + action);
            actions.Add("C" + action);

        } 
        if(atack2 != KeyCode.None)
        {
            char action = atack2.ToString().ToCharArray()[0];
            actions.Add("U" + action);
            actions.Add("F" + action);
            actions.Add("C" + action);

        }
        if (atack3 != KeyCode.None)
        {
            char action = atack3.ToString().ToCharArray()[0];
            actions.Add("U" + action);
            actions.Add("F" + action);
            actions.Add("C" + action);

        }
        if (atack4 != KeyCode.None)
        {
            char action = atack4.ToString().ToCharArray()[0];
            actions.Add("U" + action);
            actions.Add("F" + action);
            actions.Add("C" + action);

        }

        return actions;
    }
}
