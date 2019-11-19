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
    void Start()
    {
        InvokeRepeating("CheckHeald", 0, 0.8f);
    }

    // Update is called once per frame
   
    private void CheckHealt()
    {
        if (player_controller) { }
    }
}
