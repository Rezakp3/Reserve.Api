using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Activity
{
    public class DeactiveUser_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeactiveUser_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task DeactiveUser_Return200()
        {
            var response = await _client.GetAsync("/api/User/DeactiveUser/03cc13df-779f-43a2-813c-b98713b428b9");
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}

