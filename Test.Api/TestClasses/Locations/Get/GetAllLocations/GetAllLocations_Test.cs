
using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Get.GetAllLocations
{
    public class GetAllLocations_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetAllLocations_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetAll_Return302_Found()
        {
            var response = await _client.GetAsync("/api/Location/GetAll");
            var res = response.ToString();
            var result = await response.Content.ReadAsStringAsync();    
            //var result = await response.Content.ReadAsAsync<StandardResult<List<Core.Entities.Locations>>>();
            
            //HttpResponseMessage response = await _client.GetAsync("/api/Location/GetAll");
            //var result = await response.Content.ReadAsAsync<StandardResult<List<Core.Entities.Locations>>>();
            //result.Success.ShouldBeTrue();
            //result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            //result.Result
            //    .ShouldNotBeNull()
            //    .ShouldNotBeOfType<List<Core.Entities.Locations>>();
        }

        [Fact]
        public async Task GetAll_Return404_NotFound()
        {
            var response = await _client.GetAsync("/api/Location/GetAll");
            var result = await response.Content.ReadAsAsync<StandardResult<List<Core.Entities.Locations>>>();
            result.Success.ShouldBeFalse();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
            result.Result.ShouldBeNull();
        }
    }
}
