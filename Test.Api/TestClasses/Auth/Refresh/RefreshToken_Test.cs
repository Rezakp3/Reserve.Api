using FluentResults;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Auth.Refresh
{
    public class RefreshToken_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public RefreshToken_Test(ApiWebApplicationFactory<Program> apiWeb)
            :base(apiWeb)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetRefreshToken()
        {
            var refreshToken = new { RefreshToken = Guid.Parse("117f322a-8aee-478c-83c0-568a0d82fbc0") };
            var myContent = JsonConvert.SerializeObject(refreshToken);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Auth/Refresh", byteContent);
            //response.EnsureSuccessStatusCode();
            var r = response.StatusCode;
            var result = await response.Content.ReadAsAsync<Result<Core.Entities.Auth>>();
            result.IsSuccess.ShouldBeTrue();
            result.ShouldNotBeNull();
        }
    }
}
