 
using ETL_Clientes.Class;
using ETL_Clientes.Services;
using ETL_Clientes.Services.Maps;
using Microsoft.Data.SqlClient;
namespace ETL_Clientes
{
    internal class program
    {
        static void Main(string[] args)
        {
            string basepath = @"C:\Users\juand\Downloads\";
            string connectionString = "Server=MSI;Database=Clientes_opiniones_ETL4;Integrated Security=True;TrustServerCertificate=True";
            //extraccion de datos y transformacion

            //Lectura de las tablas que son FK de las demas

            //clients
            var client = readerCSVServices.LeerCsv<Clientes>(Path.Combine(basepath, "clients.csv"));
            var clientes = TransformCSV.TransformClients(client);
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"{cliente.IdCliente}, {cliente.Nombre}, {cliente.Email}");
            }
            //products
            var products = readerCSVServices.LeerCsv<Productos>(Path.Combine(basepath, "products.csv")); 
            var productos = TransformCSV.TransformProducts(products);
            foreach (var product in productos)
            {
                Console.WriteLine($"{product.IdProducto}, {product.Nombre}, {product.Categoría}");
            }

            //surveys
            var surveyse = readerCSVServices.LeerCsv<surveys, SurveysMap>(Path.Combine(basepath, "surveys_part1.csv"));
            var surveys = TransformCSV.TransformSurveys(surveyse, clientes, productos);
            foreach (var survey in surveys)
            {
                Console.WriteLine($"{survey.IdOpinion}, {survey.IdCliente}, {survey.IdProducto}, {survey.PuntajeSatisfacción}, " +
                    $"{survey.Comentario}, {survey.Fecha}, {survey.Clasificación}, {survey.Fuente}");
            } 

            //Lectura de las tablas que dependen de las anteriores

            //reviews
            var reviewss = readerCSVServices.LeerCsv<webReviews, WebReviewsMap>(Path.Combine(basepath, "web_reviews.csv"));
            var reviews = TransformCSV.TransformReviews(reviewss, clientes, productos);
            foreach (var review in reviews)
            {
                Console.WriteLine($"{review.IdReview}, {review.IdCliente}, {review.IdProducto}, {review.Rating}, {review.Comentario}, {review.Fecha}");
            }

            //comments
            var commentss = readerCSVServices.LeerCsv<Social_comments, Social_commentsMap>(Path.Combine(basepath, "social_comments.csv"));
            var comments = TransformCSV.TransformComments(commentss, clientes, productos);
            foreach (var comment in comments)
            {
                Console.WriteLine($"{comment.IdComment}, {comment.IdCliente}, {comment.IdProducto}, {comment.Fuente}, " +
                    $"{comment.Comentario}, {comment.Fecha}");
            } 

            //fuente_datos
            var fuente_datos = readerCSVServices.LeerCsv<Fuente_datos, FuenteMap>(Path.Combine(basepath, "fuente_datos.csv"));
            var fuenteDatos = TransformCSV.Transform_Fuente(fuente_datos);
            foreach (var fuente in fuenteDatos)
            {
                Console.WriteLine($"{fuente.IdFuente}, {fuente.FechaCarga}, {fuente.TipoFuente}");
            }

            //carga de datos
            Console.WriteLine("Iniciando la carga de datos en la base de datos...");
            var dbLoader = new DbLoaderServices(connectionString); 
            dbLoader.LoadData(clientes, productos, surveys, comments, reviews, fuenteDatos);

        }
    }
}
