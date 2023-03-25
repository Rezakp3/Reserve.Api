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
                UserName = "rkp"
            };

            var myContent = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Auth/Register", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<Core.Entities.Auth>>();
            result.IsSuccess.ShouldBeTrue();
            result.ValueOrDefault.AccessToken.ShouldNotBeNull();
            result.ValueOrDefault.AccessToken.ShouldNotBeEmpty();
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

            var myContent = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PostAsync("/api/Auth/Register", byteContent);
            //response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result<Core.Entities.Auth>>();
            result.IsFailed.ShouldBeTrue();
        }
    }
}
