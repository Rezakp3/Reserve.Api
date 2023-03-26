using Core.Dtos;
using Core.Entities;
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

namespace Test.Api.TestClasses.Locations.Insert
{
    public class InsertLocation_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public InsertLocation_Test(ApiWebApplicationFactory<Program> api)
            :base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Insert_ValidObjectPassed_Return200()
        {
            var loc = new Core.Entities.Locations
            {
                CreatorId = Guid.Parse("ad52d995-ba99-402c-b465-d59776aa47a4"),
                Adres = "random addres",
                Latitude = 25,
                Longitude = 16,
                LocationType = Core.Enums.LocationType.residential,
                Title = "test title"
            };

            var response = await _client.PostAsync("/api/Location/Insert", CreateContent(loc));
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
