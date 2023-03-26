using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Activity
{
    public class ActiveUser_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public ActiveUser_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task ActivateUser_Return200()
        {
            var response = await _client.GetAsync("/api/User/ActiveUser/9cc36993-8b13-4c0c-b09d-fb867f52b05a");
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
