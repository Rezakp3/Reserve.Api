using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Test.Api.Model.ConfigModel;
using Core.Entities;
using System.Net.Http.Headers;
using System.Text;
using Core.Dtos;

namespace Test.Api.Configuration
{
    public class BaseClass
    {
        private static IConfigurationRoot _configuration;
        public readonly HttpClient _client;
        public IHttpRequest _httpRequest;


        public BaseClass(ApiWebApplicationFactory<Program> factory)
        {
            if (_httpRequest == null)
                _httpRequest = new HttpRequest();
            _client = factory.CreateDefaultClient();


            var projectDir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder().AddJsonFile(
                Path.GetFullPath(Path.Combine(projectDir, "../../../Properties/integrationsettings.json")));
            _configuration = builder.Build();
        }

        public async void Login(string name)
        {
            LoginParam loginParam = new LoginParam()
            {
                userName = _configuration[$"Users:{name}:UserName"],
                password = _configuration[$"Users:{name}:Password"]
            };

            var response = _client.PostAsync("/api/Auth/Login", CreateContent(loginParam));
            var result = await response.Result.Content.ReadAsAsync<StandardResult<AuthDto>>();
            var token = result.Result.AccessToken;

            Authorization(token);
        }
        public void Authorization(string token)
        {
            // Add token to header for tests authorization
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public ByteArrayContent CreateContent<T>(T obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}
