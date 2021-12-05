using System.Net.Http.Headers;
using System.Net.Http;

namespace LyricsCalculator
{
    public static class Api
    {
        public static HttpClient? ApiClient { get; set; }

        public static void Init_ApiClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string agent = "ClientDemo/1.0.0.0";
            ApiClient.DefaultRequestHeaders.Add("User-Agent", agent);
        }
    }
}
