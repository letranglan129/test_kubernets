using RestSharp;

namespace WebUIClient.Ultis
{
    public class ProductRestAPI
    {
        protected static string connectionString = Environment.GetEnvironmentVariable("API_URL");
        public static RestClient GetClient(string path)
        {
            return new RestClient(connectionString + path);
        }
    }
}
