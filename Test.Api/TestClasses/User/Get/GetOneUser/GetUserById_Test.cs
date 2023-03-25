using FluentResults;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Get.GetOneUser
{
    public class GetUserById_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public GetUserById_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task GetOne_ValidId_Return200()
        {
            var response = await _client.GetAsync("/api/User/GetById/0cb50386-bd92-48e6-8c99-5d983f777fbf");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<Core.Entities.User>>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}


