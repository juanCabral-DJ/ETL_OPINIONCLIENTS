using CsvHelper.Configuration;
using ETL_Clientes.Class;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETL_Clientes.Services.Maps
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
                Map(m => m.FechaCarga).Name("FechaCarga")
                .Convert(row =>
                { 
                    var fechaString = row.Row.GetField<string>("FechaCarga");
                     
                    string formato = "yyyy/M/dd";
                     
                    if (DateTime.TryParseExact(fechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                    { 
                        return fecha;
                    }
                     
                    return DateTime.Now.Date;
                });
            Map(m => m.TipoFuente).Name("TipoFuente");
                
            }
          }
        
}
