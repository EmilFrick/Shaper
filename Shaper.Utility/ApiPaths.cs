using System.ComponentModel;
using System.Reflection;

namespace Shaper.Utility
{
    public static class ApiPaths
    {
        private const string root = "https://localhost:7219/api/";
        private const string Colors = $"{root}Colors/";
        private const string Shapes = $"{root}Shapes/";
        private const string Transparencies = $"{root}Transparencies/";
        private const string Products = $"{root}Products/";
        private const string ProductsVM = $"{root}Products/UpsertVM/";
        private const string ProductComponents = $"{root}Products/ProductComponents/";


        public enum ApiPath
        {
            Colors,
            Shapes,
            Transparencies,
            Products,
            ProductsVM,
            ProductComponents
        }

        public static string GetEndpoint(this ApiPath path, int? id = null)
        {
            string endpoint = GetPath(path);
            if (id == null)
            {
                return endpoint;
            }
            else
            {
                return endpoint + id.ToString();
            }
        }

        private static string GetPath(ApiPath path)
        {
            switch (path)
            {
                case ApiPath.Colors:
                    return Colors;
                case ApiPath.Shapes:
                    return Shapes;
                case ApiPath.Transparencies:
                    return Transparencies;
                case ApiPath.Products:
                    return Products;
                case ApiPath.ProductsVM:
                    return ProductsVM;
                case ApiPath.ProductComponents:
                    return ProductComponents;
                default:
                    return null;
            }
        }


    }
}
