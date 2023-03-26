using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Configuration;
using Test.Api.Model.Objects;
using Shouldly;
using Core.Entities;
using Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace Test.Api.TestClasses.Auth.Register
{
    public class RegisterNewUser_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public RegisterNewUser_Test(ApiWebApplicationFactory<Program> apiWeb)
            : base(apiWeb)
        {
        }

        [Fact]
        public async Task Register_New_User()
        {
            var user = new RegisterUserModel()
            {
                FName = "ye esm",
                LName = "keramati",
                Password = "123",
                UserName = "ajab1"
            };

            var response = await _client.PostAsync("/api/Auth/Register", CreateContent(user));
            var result = await response.Content.ReadAsAsync<StandardResult<AuthDto>>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Register_New_User_UserName_Taked_Before()
        {
            var user = new RegisterUserModel()
            {
                FName = "ye esm",
                LName = "keramati",
                Password = "123",
                UserName = "reza"
            };

            var response = await _client.PostAsync("/api/Auth/Register", CreateContent(user));
            //response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var result = await response.Content.ReadAsAsync<StandardResult<AuthDto>>();
            result.Success.ShouldBeFalse();
            result.StatusCode.ShouldBe(StatusCodes.Status406NotAcceptable);
        }
    }
}
