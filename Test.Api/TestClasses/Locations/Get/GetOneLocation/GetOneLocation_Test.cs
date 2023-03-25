using FluentResults;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;
using Test.Api.Model.Objects;

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
        public async Task GetOne_ValidId_Return200()
        {
            var response = await _client.GetAsync("/api/Location/GetById/ab9a749d-331a-42e7-95bf-4cc62202e988");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<Core.Entities.Locations>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
