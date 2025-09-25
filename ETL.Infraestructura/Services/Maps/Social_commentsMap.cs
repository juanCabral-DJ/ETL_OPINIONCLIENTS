using CsvHelper.Configuration;
using ETL_Clientes.Class;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Clientes.Services.Maps
{
    public class Social_commentsMap : ClassMap<Social_comments>
    {
        public Social_commentsMap()
        {
            Map(m => m.IdComment)
                .Name("IdComment")
                 .Convert(row =>
                 {
                     var rawId = row.Row.GetField<string>("IdComment"); // string del CSV
                     if (string.IsNullOrWhiteSpace(rawId)) return 0;
                     // quitar letras, dejar solo dígitos
                     var digits = System.Text.RegularExpressions.Regex.Replace(rawId, @"\D", "");
                     return int.TryParse(digits, out var id) ? id : 0;
                 });
            Map(m => m.IdProducto)
                .Name("IdProducto")
                .Convert(row =>
                {
                    var rawId = row.Row.GetField<string>("IdProducto"); // string del CSV
                    if (string.IsNullOrWhiteSpace(rawId)) return 0;
                    // quitar letras, dejar solo dígitos
                    var digits = System.Text.RegularExpressions.Regex.Replace(rawId, @"\D", "");
                    return int.TryParse(digits, out var id) ? id : 0;
                });
            Map(m => m.IdCliente)
                .Name("IdCliente")
                .Convert(row =>
                {
                    var rawId = row.Row.GetField<string>("IdCliente"); // string del CSV
                    if (string.IsNullOrWhiteSpace(rawId)) return 0;
                    // quitar letras, dejar solo dígitos
                    var digits = System.Text.RegularExpressions.Regex.Replace(rawId, @"\D", "");
                    return int.TryParse(digits, out var id) ? id : 0;
                });
            Map(m => m.Fuente).Name("Fuente");
            Map(m => m.Comentario).Name("Comentario");
            Map(m => m.Fecha).Name("Fecha")
                 .Convert(row =>
                 {
                     var fechaString = row.Row.GetField<string>("Fecha");

                     string formato = "yyyy/M/dd";

                     if (DateTime.TryParseExact(fechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                     {
                         return fecha;
                     }

                     return DateTime.Now.Date;
                 });
        }
    }
}
