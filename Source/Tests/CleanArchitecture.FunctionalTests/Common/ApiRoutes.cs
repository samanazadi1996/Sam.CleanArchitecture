namespace CleanArchitecture.FunctionalTests.Common;

internal static class ApiRoutes
{
    internal static class V1
    {
        internal static class Account
        {
            internal const string Authenticate = "/api/v1/Account/Authenticate";
            internal const string ChangeUserName = "/api/v1/Account/ChangeUserName";
            internal const string ChangePassword = "/api/v1/Account/ChangePassword";
            internal const string Start = "/api/v1/Account/Start";
        }

        internal static class Product
        {
            internal const string GetPagedListProduct = "/api/v1/Product/GetPagedListProduct";
            internal const string GetProductById = "/api/v1/Product/GetProductById";
            internal const string CreateProduct = "/api/v1/Product/CreateProduct";
            internal const string UpdateProduct = "/api/v1/Product/UpdateProduct";
            internal const string DeleteProduct = "/api/v1/Product/DeleteProduct";
        }
    }
    internal static string AddQueryString(this string url, string key, string value)
    {
        var separator = url.Contains("?") ? "&" : "?";
        return $"{url}{separator}{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
    }
}
