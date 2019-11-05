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

    // Start is called before the first frame update
    void Start()
    {
        //Controlador de la animacion
        animController = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        _animState = checkAnimState();
        animController.SetInteger("_animState", (int)_animState);

        if (_animState == AnimState.Floor)
        {
            if (Input.GetKey(KeyCode.Q)) //Ataque 1
            {
                animController.SetInteger("state", 1);
            }
            else if (Input.GetKey(KeyCode.W)) //Ataque 2
            {
                animController.SetInteger("state", 2);
            }
            else //Volvemos al estado inicial
            {
                animController.SetInteger("state", 0);
            }
        }
        else if (_animState == AnimState.Duck)
        {


        }

    }

    private AnimState checkAnimState()
    {

        AnimState current;

        if (Input.GetKey(KeyCode.C)) //Agachado
        {
            current = AnimState.Duck;
        }
        else //En el suelo
        {
            current = AnimState.Floor;
        }

        return current;
    }
}
