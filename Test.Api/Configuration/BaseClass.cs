using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Test.Api.Model.ConfigModel;
using FluentResults;
using Core.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Shouldly;
using System.Text;
using Test.Api.Model.Objects;

namespace Test.Api.Configuration
{
    public class BaseClass
    {
        private readonly ApiWebApplicationFactory<Program> _factory;
        private static IConfigurationRoot _configuration;
        public readonly HttpClient _client;
        public IHttpRequest _httpRequest;


        public BaseClass(ApiWebApplicationFactory<Program> factory)
        {
            if (_httpRequest == null)
                _httpRequest = new HttpRequest();
            _client = factory.CreateClient();


            var projectDir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder().AddJsonFile(
                Path.GetFullPath(Path.Combine(projectDir, "../../../Properties/integrationsettings.json")));
            _configuration = builder.Build();
        }
        public async void Login(string name)
        {

            var header = new Dictionary<string, string>();

            LoginParam loginParam = new LoginParam()
            {
                userName = _configuration[$"Users:{name}:UserName"],
                password = _configuration[$"Users:{name}:Password"]
            };

            var controllerName = "https://localhost:7104/Api/Auth/Login";

            var myContent = JsonConvert.SerializeObject(loginParam);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Auth/Login", byteContent);
            //response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<Auth>>();
            

            //var res = _httpRequest.HttpPostAsync(loginParam, controllerName, header).Result;

            //var contentStr = res.Content.ReadAsStringAsync().Result;
            //var result = JsonConvert.DeserializeObject<Result<Auth>>(contentStr);
            var token = result.ValueOrDefault.AccessToken;

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
    }
}
