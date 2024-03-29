﻿using System.Collections;
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

    public Text correctPercentageIA;
    public IA IAController;

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
           image.fillAmount = LifeController.instance.getIAHealth() / (float)LifeController.instance.maxIAHealth;
            if (IAController != null && IAController.correctPredictions > 0)
                correctPercentageIA.text = ((IAController.correctPredictions * 100 / IAController.interactions)).ToString() + " %";
            else
                correctPercentageIA.text = "0 %";
        }
        else
        {
            image.fillAmount = LifeController.instance.getPlayerHealth() / (float)LifeController.instance.maxPlayerHealth;
        }
       
    }
}
