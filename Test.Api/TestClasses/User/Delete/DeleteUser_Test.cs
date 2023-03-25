using FluentResults;
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
            var myContent = JsonConvert.SerializeObject(new { id = Guid.Parse("4cd3180f-9e14-4ab9-a781-441d58262864") });
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("/api/User/Delete", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
