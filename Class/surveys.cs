using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Clientes.Class
{
    public class surveys
    { 
        public int IdOpinion { get; set; }
         
        public int IdCliente { get; set; }
         
        public int IdProducto { get; set; }
         
        public DateTime Fecha { get; set; }
         
        public string Comentario { get; set; }
         
        public string Clasificación { get; set; }
         
        public int PuntajeSatisfacción { get; set; }
      
        public string Fuente { get; set; }
    }
}
