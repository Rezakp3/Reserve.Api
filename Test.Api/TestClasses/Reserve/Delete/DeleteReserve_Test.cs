using Core.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text;
using Test.Api.Configuration;

namespace Test.Api.TestClasses.Reserve.Delete
{
    public class DeleteReserve_Test : BaseClass , IClassFixture<ApiWebApplicationFactory<Program>>
    {
        public DeleteReserve_Test(ApiWebApplicationFactory<Program> api)
            :base(api)
        {
            Login("reza");
        }

        [Fact]
        public async Task DeleteReserve_Return200()
        {
            var myContent = JsonConvert.SerializeObject(new { reserveId = Guid.Parse("e7577c88-8b70-4c27-927f-f5deecddd583") });
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/Reserve/Delete", byteContent);
            var result = await response.Content.ReadAsAsync<StandardResult>();
            result.Success.ShouldBeTrue();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }
    }
}
