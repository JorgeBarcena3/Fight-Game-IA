using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Jump : MonoBehaviour
{
    private Transform myTransform;
    /// <summary>
    /// Posicion normal de pelea
    /// </summary>
    private Vector2 initialPosition;
    /// <summary>
    /// Posicion mas alta del salto
    /// </summary>
    private Vector2 endPosition;
    /// <summary>
    /// Altura maxima
    /// </summary>
    public float height;
    /// <summary>
    /// Tiempo que tarda en realizar la acción
    /// </summary>
    public float timeAction;
    /// <summary>
    /// Tiempo que se mantiene en el aire 
    /// </summary>
    public float timeFloating;
    /// <summary>
    ///  Variable auxiliar para controlar el tiempo de salto
    /// </summary>
    private float auxTime=0;
    /// <summary>
    /// variable auxiliar para realizar la interpolacion
    /// </summary>
    private float auxLerp = 0;
    /// <summary>
    /// Indica que esta saltando y no ha alcanzado el punto mas alto
    /// </summary>
    public bool jumping;
    /// <summary>
    /// Se encuentra en el aire en algun punto del salto
    /// </summary>
    public bool inAir;
    /// <summary>
    /// Está callend despues de saltar
    /// </summary>
    public bool falling;
    /// <summary>
    /// Está en el punto mas algido del salto
    /// </summary>
    public bool highestPoint;
    void Start()
    {
        jumping = false;
        inAir = false;
        falling = false;
        highestPoint = false;
        myTransform = GetComponent<Transform>();
        initialPosition = myTransform.position;
        endPosition = new Vector2(myTransform.position.x, myTransform.position.y + height);
    }

    // Update is called once per frame
    void Update()
    {  
        //Si está saltando y no callendo se realiza la interpolacion de subida
        if (jumping && !falling && auxTime <= timeAction)
        {
            auxLerp = auxTime/timeAction;
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxLerp);
            auxTime += Time.deltaTime;
        }
        //Cuando se alcanza el punto mas alto del salto
        if (myTransform.position.y >= endPosition.y - 0.1f)
        {
            jumping = false; highestPoint = true;
        }
        else{ highestPoint = false; }
        //Cuando se está callendo del punto mas alto se realiza la interpolacion de bajada
        if (falling && auxTime >= 0)
        {
            auxLerp = auxTime / timeAction;
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxLerp);
            auxTime -= Time.deltaTime;
        }
        //Si esta en el suelo se resetean los booleanos
        if (myTransform.position.y <= initialPosition.y + 0.1f && falling) { inAir = false; falling = false; }



    }
    /// <summary>
    /// Inicia la acción de saltar
    /// </summary>
    public void JumpOwner()
    {
        jumping = true;
        inAir = true;
    }
    /// <summary>
    /// Inicia la accion de caida
    /// </summary>
    public void FallOwner()
    {
        falling = true;
    }
}
