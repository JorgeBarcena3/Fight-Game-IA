using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tipo de jugador del cual se detectará la vida
/// </summary>
public enum TypePlayer
{
    Player,
    IA
}

/// <summary>
/// Clase para manejar las barras de vida
/// </summary>
public class FilledUI : MonoBehaviour
{
    /// <summary>
    /// Imagen que se va a modificar
    /// </summary>
    private Image image;

    /// <summary>
    /// Tipo de jugador del cual se asignará la vida
    /// </summary>
    public TypePlayer tipo;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tipo == TypePlayer.IA)
        {
           image.fillAmount = LifeController.instance.GetMachineHealt();
        }
        else
        {
            image.fillAmount = LifeController.instance.GetPlayerHealt();
        }
    }
}
