using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Clase que guarda el data record del algoritmo
/// </summary>
class DataRecord
{

    /// <summary>
    /// Total de acciones guardadas
    /// </summary>
    public int total { get; set; }

    /// <summary>
    /// Contador de acciones
    /// </summary>
    public Dictionary<string, int> counts { get; set; }

    /// <summary>
    /// Constructor por defecto
    /// </summary>
    public DataRecord()
    {
        this.total = 0;
        this.counts = new Dictionary<string, int>();
    }
};
