using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predictor de acciones
/// </summary>
public class GamePredictor
{

    /// <summary>
    /// Datos almacenados
    /// </summary>
    Dictionary<string, DataRecord> data;

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public GamePredictor()
    {
        data = new Dictionary<string, DataRecord>();
    }

    /// <summary>
    /// Registra los acciones
    /// </summary>
    /// <param name="actions">Acciones a almacenar</param>
    public void RegisterActions(string actions)
    {

        string key = actions.Substring(0, actions.Length - 1);
        char value = actions[actions.Length - 1];

        if (!data.ContainsKey(key))
        {
            data[key] = new DataRecord();
        }

        DataRecord record = data[key];
        if (!record.counts.ContainsKey(value))
        {
            record.counts[value] = 0;
        }

        record.counts[value]++;
        record.total++;

    }

    /// <summary>
    /// Devuelve la accion mas probable que va a hacer el jugador
    /// </summary>
    /// <param name="actions"></param>
    /// <returns></returns>
    public char GetMostLikely(string actions)
    {
        char bestAction = ' ';
        int highestValue = 0;
        DataRecord record;
        if (data.ContainsKey(actions))
        {
            record = data[actions];
            foreach (char action in record.counts.Keys)
            {
                if (record.counts[action] > highestValue)
                {
                    bestAction = action;
                    highestValue = record.counts[action];
                }
                else if (record.counts[action] == highestValue)
                {
                    if (Random.value <= 0.5f)
                    {
                        bestAction = action;
                        highestValue = record.counts[action];
                    }
                }
            }
        }

        return bestAction;
    }

    /// <summary>
    /// Formatea los datos como un string
    /// </summary>
    /// <returns></returns>
    public string AString()
    {
        string respuesta = "";
        foreach (string key in data.Keys)
        {
            respuesta += "\n" + key + ": ";
            DataRecord record = data[key];
            foreach (char action in record.counts.Keys)
            {
                respuesta += "  " + action + "->" + record.counts[action];
            }
        }
        return respuesta;

    }


}
