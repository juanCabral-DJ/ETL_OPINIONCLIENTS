using ETL_Clientes.Class;
using ETL_Clientes.Services;

namespace ETL_Clientes
{
    internal class program
    {
        static void Main(string[] args)
        {
            string basepath = @"C:\Users\juand\Downloads\";

            //reviews
            var reviews = readerCSVServices.LeerCsv<webReviews>(Path.Combine(basepath, "web_reviews.csv"));
            foreach (var review in reviews)
            {
                Console.WriteLine($"{review.IdReview}, {review.IdCliente}, {review.IdProducto}, {review.Rating}, {review.Comentario}, {review.Fecha}");
            }

            //surveys
            var surveys = readerCSVServices.LeerCsv<surveys>(Path.Combine(basepath, "surveys_part1.csv"));
            foreach (var survey in surveys)
            {
                Console.WriteLine($"{survey.IdOpinion}, {survey.IdCliente}, {survey.IdProducto}, {survey.PuntajeSatisfacción}, " +
                    $"{survey.Comentario}, {survey.Fecha}, {survey.Clasificación}, {survey.Fuente}");
            }

            //clients
            var client = readerCSVServices.LeerCsv<Clientes>(Path.Combine(basepath, "clients.csv"));
            foreach (var cliente in client)
            {
                Console.WriteLine($"{cliente.IdCliente}, {cliente.Nombre}, {cliente.Email}");
            }

            //comments
            var comments = readerCSVServices.LeerCsv<Social_comments>(Path.Combine(basepath, "social_comments.csv"));
            foreach (var comment in comments)
            {
                Console.WriteLine($"{comment.IdComment}, {comment.IdCliente}, {comment.IdProducto}, {comment.Fuente}, " +
                    $"{comment.Comentario}, {comment.Fecha}");
            }

            //products
            var products = readerCSVServices.LeerCsv<Productos>(Path.Combine(basepath, "products.csv"));
            foreach (var product in products)
            {
                Console.WriteLine($"{product.IdProducto}, {product.Nombre}, {product.Categoría}");
            }

            //fuente_datos
            var fuente_datos = readerCSVServices.LeerCsv<Fuente_datos>(Path.Combine(basepath, "fuente_datos.csv"));
            foreach (var fuente in fuente_datos)
            {
                Console.WriteLine($"{fuente.IdFuente}, {fuente.FechaCarga}, {fuente.TipoFuente}");
            }

        }
    }
}
 