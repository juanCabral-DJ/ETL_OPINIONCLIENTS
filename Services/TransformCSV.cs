using ETL_Clientes.Class;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETL_Clientes.Services
{
    public static class TransformCSV
    {
        private static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        public static List<Productos> TransformProducts(List<Productos> data)
        {
            // 1. Eliminar duplicados por ProductId
            var clean = data
                .GroupBy(p => p.IdProducto)
                .Select(g => g.First())
                .ToList();

            // 2. Eliminar registros nulos en PK
            clean = clean.Where(p => p.IdProducto > 0).ToList();

            // 3. Normalización de textos
            foreach (var p in clean)
            {
                p.Nombre = p.Nombre?.Trim().ToLower();
                p.Categoría = Capitalize(p.Categoría?.Trim());
            }

            return clean;
        }

        public static List<Clientes> TransformClients(List<Clientes> data)
        {
            // 1. Eliminar duplicados por IdCliente
            var clean = data
                .GroupBy(c => c.IdCliente)
                .Select(g => g.First())
                .ToList();

            // 2. Eliminar registros nulos en PK
            clean = clean.Where(c => c.IdCliente > 0).ToList();

            // 3. Normalización de textos
            foreach (var c in clean)
            {
                c.Nombre = Capitalize(c.Nombre?.Trim());
                c.Email = c.Email?.Trim().ToLower();
            }
            return clean;
        }

        public static List<Fuente_datos> Transform_Fuente(List<Fuente_datos> data)
        {
            // 1. Eliminar duplicados por IdFuente
            var clean = data
                .GroupBy(f => f.IdFuente)
                .Select(g => g.First())
                .ToList();
            // 2. Eliminar registros nulos en PK
            clean = clean.Where(f => f.IdFuente > 0).ToList();

            // 3. Normalización de textos
            foreach (var f in clean)
            {
                f.TipoFuente = Capitalize(f.TipoFuente?.Trim());

                // Normalizar fecha
                if (f.FechaCarga == DateTime.MinValue || f.FechaCarga == default)
                {
                    // Si la fecha es inválida, asignar fecha actual o una por defecto
                    f.FechaCarga = DateTime.UtcNow.Date;
                }
                else
                {
                    // Normalizar formato a solo fecha (sin hora)
                    f.FechaCarga = f.FechaCarga.Date;
                }

            }
            return clean;
        }
    }
}
