using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Reserve.Get.GetAll
{
    public class GetAllReserves_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetAllReserves_Test(ApiWebApplicationFactory<Program> api)
            :base(api)
        {
            Login("reza");
        }


        [Fact]
        public async Task GetAll_Return200()
        {
            var response = await _client.GetAsync("/api/Reserve/GetAll");
            var result = await response.Content.ReadAsAsync<StandardResult<List<Core.Entities.Reserves>>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            result.Result.ShouldBeOfType <List<Core.Entities.Reserves>>();
        }
    }
}
