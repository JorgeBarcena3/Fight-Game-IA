using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class relojTimer : MonoBehaviour
{
    /// <summary>
    /// Imagen que se va a modificar
    /// </summary>
    private Image image;    

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = GameManager.instance.aux_pulsoDeAcciones / GameManager.instance.pulsoDeAcciones;
    }
}
