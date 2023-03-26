using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Get.GetOneLocation
{
    public class GetOneLocation_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetOneLocation_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetOne_ValidId_Return302_Found()
        {
            var response = await _client.GetAsync("/api/Location/GetById/8a5e3d2d-d335-4a1a-b9bb-35a1b9fa6642");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.Locations>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            result.Result.ShouldBeOfType<Core.Entities.Locations>();
        }

        [Fact]
        public async Task GetOne_InvalidId_Return404_NotFound()
        {
            var response = await _client.GetAsync("/api/Location/GetById/ab9a785d-331a-42e7-95bf-4cc62202e988");
            var result = await response.Content.ReadAsAsync<StandardResult<Core.Entities.Locations>>();
            result.Success.ShouldBeFalse();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
            result.Result.ShouldBeNull();
        }
    }
}
