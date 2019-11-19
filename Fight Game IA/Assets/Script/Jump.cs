using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Jump : MonoBehaviour
{

    /// <summary>
    /// Si esta o no saltando
    /// </summary>
    private bool jump;

    /// <summary>
    /// Transform del componente
    /// </summary>
    private Transform myTransform;

    /// <summary>
    /// Posicion normal de pelea
    /// </summary>
    private Vector3 initialPosition;

    /// <summary>
    /// Posicion mas alta del salto
    /// </summary>
    private Vector3 endPosition;

    /// <summary>
    /// Altura maxima
    /// </summary>
    public float height;

    /// <summary>
    /// Tiempo que tarda en realizar la acción
    /// </summary>
    public float timeAction;

    /// <summary>
    /// Para el tiempo de la interpolacion
    /// </summary>
    private float t = 0;

    /// <summary>
    /// Si ha llegado a lo alto del salto
    /// </summary>
    private bool haSaltado = false;


    /// <summary>
    /// Funcion de start
    /// </summary>
    void Start()
    {
        myTransform = GetComponent<Transform>();
        initialPosition = new Vector3(myTransform.position.x, myTransform.position.y, myTransform.position.z);
        endPosition = new Vector3(myTransform.position.x, myTransform.position.y + height, myTransform.position.z);
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            t += Time.deltaTime / (timeAction / 2);
            transform.position = Vector3.Lerp(transform.position, endPosition, t);

            if (transform.position == endPosition)
            {
                endPosition = initialPosition;
                haSaltado = true;
                t = 0;
                if (haSaltado == true && transform.position == initialPosition)
                {
                    endPosition = new Vector3(myTransform.position.x, myTransform.position.y + height, myTransform.position.z); 
                    haSaltado = false;
                    jump = false;
                }
            }
        }
    }

    /// <summary>
    /// Inicia la acción de saltar
    /// </summary>
    public void JumpOwner()
    {
        jump = true;
    }
}
