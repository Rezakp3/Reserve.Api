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
