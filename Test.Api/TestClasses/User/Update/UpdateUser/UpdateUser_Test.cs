using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Update.UpdateUser
{
    public class UpdateUser_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public UpdateUser_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task UpdateUser_ValidObjectPass_Return200()
        {
            var user = new
            {
                Id = Guid.Parse("ad52d995-ba99-402c-b465-d59776aa47a4"),
                UserName = "reza",
                FName = "test",
                LName = "test"
            };

            var response = await _client.PostAsync("/api/User/Update", CreateContent(user));
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
