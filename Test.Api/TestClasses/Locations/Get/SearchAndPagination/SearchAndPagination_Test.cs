using Core.Dtos;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Locations.Get.SearchAndPagination
{
    public class SearchAndPagination_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public SearchAndPagination_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task SearchAndPagination_ValidObjectPass_Return302_Found()
        {
            var searchDto = new LocationSearchDto
            {
                LocationType = 1,
                Title = "dasd",
                pageNumber = 1,
                itemCount = 10
            };

            var response = await _client.PostAsync("/api/Location/SearchAndPagination", CreateContent(searchDto));
            var result = await response.Content.ReadAsAsync<StandardResult<List<Core.Entities.Locations>>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status302Found);
            result.Result.ShouldBeOfType<List<Core.Entities.Locations>>();
        }
    }
}
