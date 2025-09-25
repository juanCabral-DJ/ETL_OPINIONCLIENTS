using ETL_Clientes.Class; 
using System;
using System.Collections.Generic;
using System.Data; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;  

namespace ETL_Clientes.Services
{
    public class DbLoaderServices 
    {
        public readonly string _connectionString;
        public DbLoaderServices(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public void LoadData(List<surveys> surveys, List<Social_comments> social_Comments, List<Clientes> clientes,
            List<Productos> productos, List<webReviews> webReviews, List<Fuente_datos> fuente_Datos)
        {
             
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Conexión a la base de datos establecida.");

                Console.WriteLine("Iniciando la carga de datos...");
                BuildInsertClients(clientes, connection);
                BuildInsertProduct(productos, connection);
                BuildInsertFuenteDatos(fuente_Datos, connection);
                BuildInsertWebReviews(webReviews, connection);
                BuildInsertsurveys(surveys, connection);
                BuildInsertComments(social_Comments, connection);
                
            }
        }

        private void BuildInsertComments(List<Social_comments> social_Comments, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdComment", typeof(int));
            dataTable.Columns.Add("IdCliente", typeof(int));
            dataTable.Columns.Add("IdProducto", typeof(int));
            dataTable.Columns.Add("Fuente", typeof(string));
            dataTable.Columns.Add("Fecha", typeof(DateTime));
            dataTable.Columns.Add("Comentario", typeof(string)); 

            foreach (var comment in social_Comments)
            {
                dataTable.Rows.Add(comment.IdComment, comment.IdCliente, comment.IdProducto, comment.Fuente, comment.Fecha, comment.Comentario);
            }


            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "SocialComments";
                
                buildcopy.WriteToServer(dataTable);
            }
        }

        private void BuildInsertWebReviews(List<webReviews> webReviews, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdReview", typeof(int));
            dataTable.Columns.Add("IdCliente", typeof(int));
            dataTable.Columns.Add("IdProducto", typeof(int));
            dataTable.Columns.Add("Fecha", typeof(DateTime));
            dataTable.Columns.Add("Comentario", typeof(string));
            dataTable.Columns.Add("Rating", typeof(int));
            foreach (var webreview in webReviews)
            {
                dataTable.Rows.Add(webreview.IdReview, webreview.IdCliente, webreview.IdProducto, webreview.Fecha, webreview.Comentario, webreview.Rating);
            }


            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "WebReviews";

                buildcopy.WriteToServer(dataTable);
            }
        }

        private void BuildInsertFuenteDatos(List<Fuente_datos> fuente_Datos, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdFuente", typeof(int)); 
            dataTable.Columns.Add("TipoFuente", typeof(string));
            dataTable.Columns.Add("FechaCarga", typeof(DateTime));
            foreach (var fuente in fuente_Datos)
            {
                dataTable.Rows.Add(fuente.IdFuente, fuente.TipoFuente, fuente.FechaCarga);
            }


            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "Fuente_Datos";

                buildcopy.WriteToServer(dataTable);
            }
        }

        private void BuildInsertProduct(List<Productos> productos, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdProducto", typeof(int));
            dataTable.Columns.Add("Nombre", typeof(string));
            dataTable.Columns.Add("Categoría", typeof(string));
            foreach (var product in productos)
            {
                dataTable.Rows.Add(product.IdProducto, product.Nombre, product.Categoría);
            }

            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "Productos";

                buildcopy.WriteToServer(dataTable);
            }
        }

        private void BuildInsertClients(List<Clientes> clientes, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdCliente", typeof(int));
            dataTable.Columns.Add("Nombre", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            foreach (var client in clientes)
            {
                dataTable.Rows.Add(client.IdCliente, client.Nombre, client.Email);
            }

            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "Clientes";

                buildcopy.WriteToServer(dataTable);
            }
        }

        private void BuildInsertsurveys(List<surveys> surveys, SqlConnection connection)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("IdOpinion", typeof(int));
            dataTable.Columns.Add("IdCliente", typeof(int));
            dataTable.Columns.Add("IdProducto", typeof(int));
            dataTable.Columns.Add("Fecha", typeof(DateTime));
            dataTable.Columns.Add("Comentario", typeof(string));
            dataTable.Columns.Add("Clasificación", typeof(string));
            dataTable.Columns.Add("PuntajeSatisfacción", typeof(int));
            dataTable.Columns.Add("Fuente", typeof(string)); 
            foreach (var survey in surveys)
            {
                dataTable.Rows.Add(survey.IdOpinion, survey.IdCliente, survey.IdProducto, survey.Fecha, survey.Comentario,
                    survey.Clasificación, survey.PuntajeSatisfacción, survey.Fuente);
            }

            using (var buildcopy = new SqlBulkCopy(connection))
            {
                buildcopy.DestinationTableName = "Surveys";
                buildcopy.WriteToServer(dataTable);
            }
        }
    }
}
