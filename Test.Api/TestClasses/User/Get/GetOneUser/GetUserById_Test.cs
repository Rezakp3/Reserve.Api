using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Get.GetOneUser
{
    public class GetUserById_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetUserById_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetOne_ValidId_Return200()
        {
            var response = await _client.GetAsync("/api/User/GetById/0cb50386-bd92-48e6-8c99-5d983f777fbf");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.User>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            result.Result.ShouldBeOfType<Core.Entities.User>();
        }
    }
}


