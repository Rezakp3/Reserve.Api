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
                Id = Guid.Parse("ab9a749d-331a-42e7-95bf-4cc62202e988"),
                Adres = "random addres",
                Latitude = 25,
                Longitude = 16,
                LocationType = Core.Enums.LocationType.residential,
                Title = "test title"
            };


            var myContent = JsonConvert.SerializeObject(loc);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PutAsync("/api/Locatios/Update", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
