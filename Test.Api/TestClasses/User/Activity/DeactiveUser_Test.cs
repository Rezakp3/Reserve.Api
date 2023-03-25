using FluentResults;
using Shouldly;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Activity
{
    public class DeactiveUser_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeactiveUser_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task DeactiveUser_Return200()
        {
            var response = await _client.GetAsync("/api/User/DeactiveUser/9cc36993-8b13-4c0c-b09d-fb867f52b05a");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}

