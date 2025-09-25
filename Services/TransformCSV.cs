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
            .GroupBy(c => new { c.IdCliente, Email = c.Email?.Trim().ToLower() })
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

                // Normalizar fecha de carga

                if (f.FechaCarga == DateTime.MinValue || f.FechaCarga == default)
                {
                    // Si la fecha es inválida, asignar fecha actual o una por defecto
                    f.FechaCarga = DateTime.UtcNow.Date;
                } 

            }
            return clean;
        }

        public static List<webReviews> TransformReviews(List<webReviews> data)
        {
            // 1. Eliminar duplicados por Idreview
            var clean = data
                .GroupBy(r => r.IdReview)
                .Select(g => g.First())
                .ToList();
            // 2. Eliminar registros nulos en PK
            clean = clean.Where(r => r.IdReview > 0 && r.IdCliente > 0 && r.IdProducto > 0).ToList();

            // 3. Normalización de datos
            foreach (var r in clean)
            {
                r.Comentario = Capitalize(r.Comentario?.Trim());
                r.Rating = r.Rating < 1 || r.Rating > 5 ? 3 : r.Rating; // Asegurar que el rating esté entre 1 y 5

                // Normalizar fecha
                if (r.Fecha == DateTime.MinValue || r.Fecha == default)
                {
                    // Si la fecha es inválida, asignar fecha actual o una por defecto
                    r.Fecha = DateTime.UtcNow.Date;
                }
                else
                {
                    // Normalizar formato a solo fecha (sin hora)
                    r.Fecha = r.Fecha.Date;
                }
            }
            return clean;
        }

        public static List<surveys> TransformSurveys(List<surveys> data)
        {
            // 1. Eliminar duplicados por IdOpinion
            var clean = data
                .GroupBy(s => s.IdOpinion)
                .Select(g => g.First())
                .ToList();
            // 2. Eliminar registros nulos en PK
            clean = clean.Where(s => s.IdOpinion > 0 && s.IdProducto > 0 && s.IdCliente > 0).ToList();
            // 3. Normalización de datos
            foreach (var s in clean)
            {
                s.Comentario = Capitalize(s.Comentario?.Trim());
                s.Clasificación = Capitalize(s.Clasificación?.Trim());
                s.Fuente = Capitalize(s.Fuente?.Trim());
                s.PuntajeSatisfacción = s.PuntajeSatisfacción < 1 || s.PuntajeSatisfacción > 10 ? 5 : s.PuntajeSatisfacción; // Asegurar que el puntaje esté entre 1 y 10
                
                // Normalizar fecha
                if (s.Fecha == DateTime.MinValue || s.Fecha == default)
                {
                    // Si la fecha es inválida, asignar fecha actual o una por defecto
                    s.Fecha = DateTime.UtcNow.Date;
                }
                else
                {
                    // Normalizar formato a solo fecha (sin hora)
                    s.Fecha = s.Fecha.Date;
                }
            }
            return clean;
        }

        public static List<Social_comments> TransformComments(List<Social_comments> data)
        {
            // 1. Eliminar duplicados por IdComment
            var clean = data
                .GroupBy(c => c.IdComment)
                .Select(g => g.First())
                .ToList();
            // 2. Eliminar registros nulos en PK
            clean = clean.Where(c => c.IdComment > 0 && c.IdCliente > 0 && c.IdProducto > 0).ToList();
            // 3. Normalización de datos
            foreach (var c in clean)
            {
                c.Comentario = Capitalize(c.Comentario?.Trim());
                c.Fuente = Capitalize(c.Fuente?.Trim());
                // Normalizar fecha
                if (c.Fecha == DateTime.MinValue || c.Fecha == default)
                {
                    // Si la fecha es inválida, asignar fecha actual o una por defecto
                    c.Fecha = DateTime.UtcNow.Date;
                }
                else
                {
                    // Normalizar formato a solo fecha (sin hora)
                    c.Fecha = c.Fecha.Date;
                }
            }
            return clean;
        }

    }
}
