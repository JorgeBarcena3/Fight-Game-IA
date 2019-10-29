using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Variable auxiliar para realizar la interpolacion
    private float auxTime=0;
    // Start is called before the first frame update
    private bool jumping;
    void Start()
    {
        jumping = false;
        myTransform = GetComponent<Transform>();
        initialPosition = myTransform.position;
        endPosition = new Vector2(myTransform.position.x, myTransform.position.y + height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpOwner()
    {
        jumping = true;
        while (auxTime <= timeAction)
        {
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxTime);
            auxTime += Time.deltaTime; 
        }
    }
    public void FallOwner()
    {
        while (auxTime >= 0)
        {
            myTransform.position = Vector2.Lerp(initialPosition, endPosition, auxTime);
            auxTime -= Time.deltaTime;
        }
        jumping = false;
    }
    public bool GetJumping()
    {
        return jumping;
    }
}
