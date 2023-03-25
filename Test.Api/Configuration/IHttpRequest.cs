using Newtonsoft.Json;
using System.Text;

namespace Test.Api.Configuration
{
    public interface IHttpRequest
    {
        Task<HttpResponseMessage> HttpPostAsync<TRequest>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
        Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null);
    }

    public class HttpRequest : IHttpRequest
    {
        private readonly HttpClient _httpClient;

        public HttpRequest()
        {
            _httpClient = new HttpClient();
        }

        private void AddHeader(Dictionary<string, string> header = null)
        {
            _httpClient.DefaultRequestHeaders.Clear();

            if (header != null)
                foreach (var item in header)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
        }


        public async Task<TResponse> HttpPostAsync<TRequest, TResponse>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string responsee = await response.Content.ReadAsStringAsync();

            TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return res;
        }
        public async Task<HttpResponseMessage> HttpPostAsync<TRequest>(TRequest requestParameter, string url, Dictionary<string, string> headers = null)
        {
            AddHeader(headers);

            string JsonPaymentCode = JsonConvert.SerializeObject(requestParameter);

            StringContent content = new StringContent(JsonPaymentCode, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            string responsee = await response.Content.ReadAsStringAsync();

            //TResponse res = JsonConvert.DeserializeObject<TResponse>(responsee);

            return response;
        }

    }
}
