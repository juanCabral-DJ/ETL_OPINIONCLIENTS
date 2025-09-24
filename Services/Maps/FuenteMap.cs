using CsvHelper.Configuration;
using ETL_Clientes.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETL_Clientes.Services
{
        public sealed class FuenteMap : ClassMap<Fuente_datos>
        {
            public FuenteMap()
            {
                Map(m => m.IdFuente)
                    .Name("IdFuente") // nombre de la columna en el CSV
                    .Convert(row =>
                    {
                        var rawId = row.Row.GetField<string>("IdFuente"); // string del CSV
                        if (string.IsNullOrWhiteSpace(rawId)) return 0;

                        // quitar letras, dejar solo dígitos
                        var digits = Regex.Replace(rawId, @"\D", "");
                        return int.TryParse(digits, out var id) ? id : 0;
                    });

                Map(m => m.TipoFuente).Name("TipoFuente");
                Map(m => m.FechaCarga).Name("FechaCarga");
            }
          }
        
}
