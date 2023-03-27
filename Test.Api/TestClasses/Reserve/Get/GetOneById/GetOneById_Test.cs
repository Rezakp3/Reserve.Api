using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Reserve.Get.GetOneById
{
    public class GetOneById_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetOneById_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetOne_Return200()
        {
            var response = await _client.GetAsync("/api/Reserve/GetById/b28aa75a-58ae-4561-8a0d-20546424f8bc");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.Reserves>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
        }
    }
}
