using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;
using Test.Api.Model.ConfigModel;

namespace Test.Api.TestClasses.Auth.Refresh
{
    public class RefreshToken_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public RefreshToken_Test(ApiWebApplicationFactory<Program> apiWeb)
            :base(apiWeb)
        {

        }

        [Fact]
        public async Task GetRefreshToken()
        {
            LoginParam loginParam = new LoginParam()
            {
                userName = "reza",
                password = "123"
            };

            var loginResponse = _client.PostAsync("/api/Auth/Login", CreateContent(loginParam));
            var loginResult = await loginResponse.Result.Content.ReadAsAsync<StandardResult<AuthDto>>();
            var token = loginResult.Result.AccessToken;

            Authorization(token);

            var refreshToken = new { loginResult.Result.RefreshToken };

            var response = await _client.PostAsync("/api/Auth/Refresh", CreateContent(refreshToken));
            var result = await response.Content.ReadAsAsync<StandardResult<AuthDto>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
