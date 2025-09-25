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
    public class SurveysMap : ClassMap<surveys>
    {
        public SurveysMap()
        {
            Map(m => m.IdOpinion)
                .Name("IdOpinion");
            Map(m => m.IdProducto)
                .Name("IdProducto");
            Map(m => m.IdCliente)
                .Name("IdCliente"); 
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
            Map(m => m.Clasificación).Name("Clasificación");
            Map(m => m.PuntajeSatisfacción).Name("PuntajeSatisfacción");
        }
    }
}
