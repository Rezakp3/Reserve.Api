using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Update
{
    public class UpdateLocation_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public UpdateLocation_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Update_ValidObjectPass_Return200()
        {
            var loc = new 
            {
                Id = Guid.Parse("1ce36dd3-6441-447d-a622-cf082322438d"),
                Adres = "random addres",
                Latitude = 25,
                Longitude = 16,
                LocationType = Core.Enums.LocationType.residential,
                Title = "test title"
            };

            var response = await _client.PutAsync("/api/Location/Update", CreateContent(loc));
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
