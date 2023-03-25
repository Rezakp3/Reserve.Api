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

namespace Test.Api.TestClasses.Locations.Delete
{
    public class DeleteOne_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeleteOne_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Delete_ValidIdPass_Return200()
        {
            var myContent = JsonConvert.SerializeObject(new { id = Guid.Parse("c12101ba-d3cc-4497-a78d-eba6fdf0dea9") });
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/Locatios/Ddelete", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
