using FluentResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Reserve.Api.Test.Configuration;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;

namespace Reserve.Api.Test.TestClasses.Auth.Refresh
{
    public class RefreshToken_Test :  IClassFixture<ApiWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiWebAppFactory<Program> _factory;
        public RefreshToken_Test(ApiWebAppFactory<Program> apiWeb)
        {
            _factory = apiWeb;
            _client = apiWeb.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetRefreshToken()
        {
            var id = new { Id = Guid.Parse("0cb50386-bd92-48e6-8c99-5d983f777fbf") };
            var myContent = JsonConvert.SerializeObject(id);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Auth/Register", byteContent);
            //response.EnsureSuccessStatusCode();
            var r = response.StatusCode;
            var result = await response.Content.ReadAsAsync<Result<Guid>>();
            result.IsSuccess.ShouldBeTrue();
            result.ShouldNotBeNull();
        }
    }
}
