using Core.Dtos;
using Core.Entities;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Exceptions;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Update.ChangePassword
{
    public class UpdatePassword : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public UpdatePassword(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task ChangePassword_Return200()
        {
            var pass = new
            {
                Id = Guid.Parse("ad52d995-ba99-402c-b465-d59776aa47a4"),
                Password = "123"
            };

            var response = await _client.PatchAsync("/api/User/ChangePassword", CreateContent(pass));
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
