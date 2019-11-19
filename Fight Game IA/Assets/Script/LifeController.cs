using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject machine;
    private CharacterAnimatorController player_controller;
    private IA machine_controller;
    private int player_healt;
    private int machine_healt;
    void Start()
    {
        player_healt = 100;
        machine_healt = 100;
        
    }

    // Update is called once per frame
   
    private void CheckHealt()
    {
        if (1 != 2)
        {
            if (true)
            {
                player_healt -= 10;
            }
            else
            {
                machine_healt -= 10;
            }
            

        }
    }
}
