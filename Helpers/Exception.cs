using Microsoft.AspNetCore.Http;

namespace AspNetCoreWebAPI.Helpers
{
    public static class Exception
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Acess-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Acess-Control-Allow-Origin", "*");
        }
    }
}