using Core.Entities;
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
                CreatorId = Guid.Parse("0cb50386-bd92-48e6-8c99-5d983f777fbf"),
                Adres = "random addres",
                Latitude = 25,
                Longitude = 16,
                LocationType = Core.Enums.LocationType.residential,
                Title = "test title"
            };


            var myContent = JsonConvert.SerializeObject(new { location = loc});
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Locatios/Insert", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
