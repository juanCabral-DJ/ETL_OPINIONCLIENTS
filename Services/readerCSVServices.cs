using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Clientes.Services
{
    public static class readerCSVServices 
    {
        public static List<T> LeerCsv<T>(string pathFile) 
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    using (var reader = new StreamReader(pathFile))
                    using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<T>().ToList();
                        return records;
                    }
                }
                else
                {
                    throw new FileNotFoundException("El archivo no existe.", pathFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return new List<T>();
            }
        }

        public static List<T> LeerCsv<T, MapT>(string pathFile) where MapT : ClassMap<T>
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    using (var reader = new StreamReader(pathFile))
                    using (var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<MapT>();
                        var records = csv.GetRecords<T>().ToList();
                        return records;
                    }
                }
                else
                {
                    throw new FileNotFoundException("El archivo no existe.", pathFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
