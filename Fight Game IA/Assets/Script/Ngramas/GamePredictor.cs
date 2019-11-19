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
    public void RegisterActions(List<string> actions)
    {
     
        string key = ListToStringAction(actions);

        string value = actions[actions.Count - 1];

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
    /// Genera una string de las lista de acciones
    /// </summary>
    /// <param name="actions">Lista de acciones</param>
    /// <returns></returns>
    private static string ListToStringAction(List<string> actions)
    {
        string actionString = "";

        foreach (string action in actions)
        {
            actionString += action;
        }

        return actionString;
    }

    /// <summary>
    /// Devuelve la accion mas probable que va a hacer el jugador
    /// </summary>
    /// <param name="actions"></param>
    /// <returns></returns>
    public string GetMostLikely(List<string> actions)
    {
        string bestAction = " ";

        int highestValue = 0;

        DataRecord record;

        string actionString = ListToStringAction(actions);

        if (data.ContainsKey(actionString))
        {
            record = data[actionString];
            foreach (string action in record.counts.Keys)
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
    
  
}
