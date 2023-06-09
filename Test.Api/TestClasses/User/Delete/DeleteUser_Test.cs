﻿using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.User.Delete
{
    public class DeleteUser_Test : BaseClass, IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeleteUser_Test(ApiWebApplicationFactory<Program> api)
            : base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task Delete_ValidIdPass_Return200()
        {
            var myContent = JsonConvert.SerializeObject(new { id = Guid.Parse("03cc13df-779f-43a2-813c-b98713b428b9") });
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/User/Delete", byteContent);
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
