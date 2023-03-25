using FluentResults;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Get.GetAllUsers
{
    public class GetAllUsers_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetAllUsers_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetAll_Return200()
        {
            var response = await _client.GetAsync("/api/User/GetAll");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<List<Core.Entities.User>>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
