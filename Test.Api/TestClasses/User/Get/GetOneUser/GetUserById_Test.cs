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
            var response = await _client.GetAsync("/api/User/GetById/ad52d995-ba99-402c-b465-d59776aa47a4");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.User>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
        }
    }
}


