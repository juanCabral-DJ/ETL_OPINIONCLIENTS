﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Clientes.Class
{
    public class Social_comments
    {
        public string IdComment { get; set; }
        public string IdCliente { get; set; }    
        public string IdProducto { get; set; }     
        public string Fuente { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }  
    }
}
