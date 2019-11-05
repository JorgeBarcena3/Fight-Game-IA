using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayer : MonoBehaviour
{
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
        if(GetComponent<Jump>())
            myJumper = GetComponent<Jump>();

        auxTimeJump = 0;
        auxTime = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Acciones de movimiento vertical
        if (Input.GetKeyDown(jump)) { myJumper.JumpOwner(); }
        else if (Input.GetKeyDown(crouch)) {/*TODO*/ }
        else { }

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

        //Acciones de ataque
        if (!atacking && (myJumper.highestPoint || !myJumper.inAir))
        {
            if (Input.GetKeyDown(atack1)) { atacking = true; GetComponent<SpriteRenderer>().color = Color.red; }
            else if (Input.GetKeyDown(atack2)) { atacking = true; GetComponent<SpriteRenderer>().color = Color.green; }
            else if (Input.GetKeyDown(atack3)) { atacking = true; GetComponent<SpriteRenderer>().color = Color.yellow; }
            else if (Input.GetKeyDown(atack4)) { atacking = true; GetComponent<SpriteRenderer>().color = Color.blue; }
            else { GetComponent<SpriteRenderer>().color = Color.white; }
        }
       

    }
}
