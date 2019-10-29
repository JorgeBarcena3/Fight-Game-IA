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
    private Jump myJumper;
    private bool atacking;


    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Jump>())
            myJumper = GetComponent<Jump>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Acciones de movimiento vertical
        if (Input.GetKeyDown(jump)) { myJumper.JumpOwner(); }
        else if (Input.GetKeyDown(crouch)) {/*TODO*/ }
        else {/*TODO*/ }

        //Acciones de ataque
        if (Input.GetKeyDown(atack1)) { GetComponent<SpriteRenderer>().color = Color.red; }
        else if (Input.GetKeyDown(atack2)) { GetComponent<SpriteRenderer>().color = Color.green; }
        else if (Input.GetKeyDown(atack3)) { GetComponent<SpriteRenderer>().color = Color.yellow; }
        else if (Input.GetKeyDown(atack4)) { GetComponent<SpriteRenderer>().color = Color.blue; }
        else { GetComponent<SpriteRenderer>().color = Color.white; }

    }
}
