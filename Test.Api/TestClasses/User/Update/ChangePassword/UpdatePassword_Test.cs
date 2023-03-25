using Core.Entities;
using FluentResults;
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
                Id = Guid.Parse("4cd3180f-9e14-4ab9-a781-441d58262864"),
                Password = "456"
            };


            var myContent = JsonConvert.SerializeObject(pass);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PatchAsync("/api/User/ChangePassword", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
