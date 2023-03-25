using FluentResults;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Get.GetAllLocations
{
    public class GetAllLocations_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetAllLocations_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetAll_Return200()
        {
            var response = await _client.GetAsync("/api/Location/GetAll");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<List<Core.Entities.Locations>>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
