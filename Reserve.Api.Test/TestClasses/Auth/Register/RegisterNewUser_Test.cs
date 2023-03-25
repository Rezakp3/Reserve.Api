using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Reserve.Api.Test.Configuration;
using Reserve.Api.Test.Model.Objects;
using Shouldly;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Reserve.Api.Test.TestClasses.Auth.Register
{
    public class RegisterNewUser_Test : IClassFixture<ApiWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ApiWebAppFactory<Program> _factory;
        public RegisterNewUser_Test(ApiWebAppFactory<Program> apiWeb)
        {
            _factory = apiWeb;
            _client = apiWeb.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
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
            var data = new StringContent(myContent, Encoding.UTF8, "application/json");
            var url = "/api/Auth/Register";
            var response = await _client.PostAsync(url, data);
            //response.EnsureSuccessStatusCode();
            var sc = response.StatusCode;
            //response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            //var ajab = await response.Content.ReadAsAsync<Result<Core.Entities.Auth>>();
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
