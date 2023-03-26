using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Delete
{
    public class DeleteLocation_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeleteLocation_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Delete_ValidIdPass_Return200()
        {
            var locationId = JsonConvert.SerializeObject(new { id = Guid.Parse("c12101ba-d3cc-4497-a78d-eba6fdf0dea9") });

            var response = await _client.PostAsync("/api/Location/Delete", CreateContent(locationId));
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Success.ShouldBeTrue();
        }
    }
}
