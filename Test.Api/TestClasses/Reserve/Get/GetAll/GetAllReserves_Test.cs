using FluentResults;
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
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<List<Core.Entities.Reserves>>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
