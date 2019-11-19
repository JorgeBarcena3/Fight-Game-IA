using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Jump : MonoBehaviour
{
    private Transform myTransform;
    //Posicion normal de pelea
    private Vector2 initialPosition;
    //Posicion mas alta del salto
    private Vector2 endPosition;
    //Altura maxima
    public float height;
    //Tiempo que tarda en realizar la acción
    public float timeAction;
    //Tiempo que se mantiene en el aire 
    public float timeFloating;
    //Variable auxiliar para controlar el tiempo de salto
    private float auxTime=0;
    //variable auxiliar para realizar la interpolacion
    private float auxLerp = 0;
    
    public bool jumping;
    public bool inAir;
    public bool falling;
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
        if (jumping && !falling && auxTime <= timeAction)
        {
            auxLerp = auxTime/timeAction;
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxLerp);
            auxTime += Time.deltaTime;
        }
        if (myTransform.position.y >= endPosition.y - 0.1f)
        {
            jumping = false; highestPoint = true;
        }
        else{ highestPoint = false; }

        if (falling && auxTime >= 0)
        {
            auxLerp = auxTime / timeAction;
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxLerp);
            auxTime -= Time.deltaTime;
        }
        if (myTransform.position.y <= initialPosition.y + 0.1f && falling) { inAir = false; falling = false; }



    }
    public void JumpOwner()
    {
        jumping = true;
        inAir = true;
    }
    public void FallOwner()
    {
        falling = true;
    }
    public bool GetJumping()
    {
        return jumping;
    }
}
