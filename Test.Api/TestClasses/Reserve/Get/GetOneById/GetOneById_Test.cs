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
            var response = await _client.GetAsync("/api/Reserve/GetById/e7577c88-8b70-4c27-927f-f5deecddd583");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.Reserves>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            result.Result.ShouldBeOfType<Core.Entities.Reserves>();
        }
    }
}
