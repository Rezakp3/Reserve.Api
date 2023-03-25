using FluentResults;
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
                Id = Guid.Parse("4cd3180f-9e14-4ab9-a781-441d58262864"),
                UserName = "test",
                FName = "test",
                LName = "test"
            };


            var myContent = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            var response = await _client.PutAsync("/api/User/Update", byteContent);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<Result>();
            result.IsSuccess.ShouldBeTrue();
        }
    }
}
