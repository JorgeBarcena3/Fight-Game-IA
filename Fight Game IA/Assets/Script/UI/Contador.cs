using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Muestra el contador por pantalla
/// </summary>
public class Contador : MonoBehaviour
{
    /// <summary>
    /// Texto donde se va a escribir
    /// </summary>
    private Text texto;
    
    // Start is called before the first frame update
    void Start()
    {
        texto = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = GameManager.instance.getFormatTime();
    }
}
